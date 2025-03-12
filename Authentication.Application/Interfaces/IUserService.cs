using Authentication.Application.Requests;
using Authentication.Application.Results;
using Authentication.Domain.DTOs;

namespace Authentication.Application.Interfaces;
public interface IUserService
{
    Task<Result<UserDTO>> CreateUserAsync(CreateUserRequest request);
}
