using Authentication.Application.Requests;
using Authentication.Application.Responses;
using Authentication.Domain.DTOs;

namespace Authentication.Application.Interfaces;
public interface IUserService
{
    Task<Result<AuthenticateResponse>> AuthenticateAsync(AuthenticateRequest request);
    Task<Result<UserDTO>> CreateUserAsync(CreateUserRequest request);
    Task<Result<AuthenticateResponse>> RefreshTokensAsync(RefreshTokenRequest request);
    Task<Result> VerifyUserEmailAsync(Guid token);
}
