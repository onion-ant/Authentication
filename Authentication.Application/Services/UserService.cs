using Authentication.Application.Extensions.Mappings;
using Authentication.Application.Interfaces;
using Authentication.Application.Requests;
using Authentication.Application.Responses;
using Authentication.Domain.DTOs;
using Authentication.Infrastructure.Interfaces;

namespace Authentication.Application.Services;
public class UserService(IUserRepository userRepository, IHashService hashService, ITokenService tokenService) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IHashService _hashService = hashService;
    private readonly ITokenService _tokenService = tokenService;

    public async Task<Result<AuthenticateResponse>> AuthenticateAsync(AuthenticateRequest request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user == null)
            return Result.Failure<AuthenticateResponse>(Error.BadRequest("Incorrect email and/or password"));
        var checkPassword = _hashService.VerifyHash(request.Password, user.PasswordHash);
        if (!checkPassword)
            return Result.Failure<AuthenticateResponse>(Error.BadRequest("Incorrect email and/or password"));
        var userDTO = user.ToUserDTO();
        var token = _tokenService.GetToken(userDTO);
        return Result.Success(userDTO.ToAuthenticateResponse(token));
    }

    public async Task<Result<UserDTO>> CreateUserAsync(CreateUserRequest request)
    {
        var userExists = await _userRepository.ExistsByEmailAsync(request.Email);
        if (userExists)
            return Result.Failure<UserDTO>(Error.BadRequest("There is already a user with this email"));
        //Validar Email
        var passwordHash = _hashService.GenerateHash(request.Password);
        var userCreated = await _userRepository.CreateUserAsync(request.ToUser(passwordHash));
        return Result.Success(userCreated.ToUserDTO());
    }
}
