using Authentication.Application.Extensions.Mappings;
using Authentication.Application.Interfaces;
using Authentication.Application.Requests;
using Authentication.Application.Responses;
using Authentication.Domain.DTOs;
using Authentication.Domain.Entities;
using Authentication.Infrastructure.Interfaces;
using FluentEmail.Core;

namespace Authentication.Application.Services;
public class UserService(IUserRepository userRepository, IHashService hashService, ITokenService tokenService, IFluentEmail fluentEmail, IUnitOfWork unitOfWork, IEmailRepository emailRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IHashService _hashService = hashService;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IFluentEmail _fluentEmail = fluentEmail;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IEmailRepository _emailRepository = emailRepository;

    public async Task<Result<AuthenticateResponse>> AuthenticateAsync(AuthenticateRequest request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user == null)
            return Result.Failure<AuthenticateResponse>(Error.BadRequest("Incorrect email and/or password"));
        var checkPassword = _hashService.VerifyHash(request.Password, user.PasswordHash);
        if (!checkPassword)
            return Result.Failure<AuthenticateResponse>(Error.BadRequest("Incorrect email and/or password"));
        if (!user.Verified)
            return Result.Failure<AuthenticateResponse>(Error.BadRequest("Email not verified"));
        var userDTO = user.ToUserDTO();
        var token = _tokenService.GetToken(userDTO);
        var refreshToken = _tokenService.GetRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        await _userRepository.UpdateUserAsync(user);
        return Result.Success(userDTO.ToAuthenticateResponse(token, refreshToken));
    }

    public async Task<Result<UserDTO>> CreateUserAsync(CreateUserRequest request)
    {
        var userExists = await _userRepository.ExistsByEmailAsync(request.Email);
        if (userExists)
            return Result.Failure<UserDTO>(Error.BadRequest("There is already a user with this email"));
        var passwordHash = _hashService.GenerateHash(request.Password);
        _unitOfWork.BeginTransaction();
        var userCreated = await _userRepository.CreateUserAsync(request.ToUser(passwordHash));
        var userDto = userCreated.ToUserDTO();
        var verificationToken = userDto.ToEmailVerificationToken();
        await _emailRepository.CreateEmailTokenAsync(verificationToken);
        await _fluentEmail.To(request.Email)
            .Subject("Email verification")
            .Body($"Your email verify code is {verificationToken.Id}", isHtml: true)
            .SendAsync();
        await _unitOfWork.CommitAsync();
        return Result.Success(userDto);
    }

    public async Task<Result<AuthenticateResponse>> RefreshTokensAsync(RefreshTokenRequest request)
    {
        var user = await _userRepository.GetByIdAsync(request.UserID);
        if (user == null)
            return Result.Failure<AuthenticateResponse>(Error.BadRequest("User does not exists"));
        if(user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime < DateTime.UtcNow)
            return Result.Failure<AuthenticateResponse>(Error.BadRequest("Invalid refresh token"));
        var userDTO = user.ToUserDTO();
        var token = _tokenService.GetToken(userDTO);
        var refreshToken = _tokenService.GetRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        await _userRepository.UpdateUserAsync(user);
        return Result.Success(userDTO.ToAuthenticateResponse(token, refreshToken));
    }

    public async Task<Result> VerifyUserEmailAsync(Guid token)
    {
        var verificationToken = await _emailRepository.GetEmailTokenAsync(token);
        if (verificationToken == null || verificationToken.ExpiresOnUtc < DateTime.UtcNow)
            return Result.Failure(Error.BadRequest("Invalid verification token"));
        _unitOfWork.BeginTransaction();
        await _userRepository.VerifyUserEmailAsync(verificationToken.UserId);
        await _emailRepository.DeleteEmailTokenAsync(verificationToken);
        await _unitOfWork.CommitAsync();
        return Result.Success();
    }
}
