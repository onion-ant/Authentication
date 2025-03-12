using Authentication.Application.Requests;
using Authentication.Application.Responses;
using Authentication.Domain.DTOs;
using Authentication.Domain.Entities;

namespace Authentication.Application.Extensions.Mappings;
public static class UserMappings
{
    public static AuthenticateResponse ToAuthenticateResponse(this UserDTO user, string token)
    {
        return new AuthenticateResponse 
        { 
            Token = token, 
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
}
