using Authentication.Application.Interfaces;
using Authentication.Application.Requests;
using Authentication.Application.Results;
using Authentication.Domain.DTOs;
using Authentication.Infrastructure.Interfaces;

namespace Authentication.Application.Services;
public class UserService(IUserRepository userRepository, IHashService hashService) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IHashService _hashService = hashService;

    public async Task<Result<UserDTO>> CreateUserAsync(CreateUserRequest request)
    {
        var userExists = await _userRepository.UserExistsByEmailAsync(request.Email);
        if(userExists)
            return Result.Failure<UserDTO>(Error.BadRequest("There is already a user with this email"));
        //Validar Email
        var passwordHash = _hashService.GenerateHash(request.Password);
        var userCreated = await _userRepository.CreateUserAsync(request.ToUser(passwordHash));
        return Result.Success(userCreated.ToUserDTO());
    }
}
