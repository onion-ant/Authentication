using Authentication.Application.Requests;
using Authentication.Application.Responses;
using Authentication.Domain.DTOs;
using Authentication.Domain.Entities;

namespace Authentication.Application.Extensions.Mappings;
public static class UserMappings
{
    public static AuthenticateResponse ToAuthenticateResponse(this UserDTO user, string token, string refreshToken)
    {
        return new AuthenticateResponse 
        { 
            Token = token, 
            RefreshToken = refreshToken,
            User = user
        };
    }
    public static UserDTO ToUserDTO(this User user)
    {
        return new UserDTO()
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role,
        };
    }
    public static User ToUser(this CreateUserRequest user, string passwordHash)
    {
        return new User()
        {
            Name = user.Name,
            Email = user.Email,
            PasswordHash = passwordHash,
            Role = user.Role,
        };
    }
    public static EmailVerificationToken ToEmailVerificationToken(this UserDTO user)
    {
        return new EmailVerificationToken()
        {
            UserId = user.Id,
        };
    }
}
