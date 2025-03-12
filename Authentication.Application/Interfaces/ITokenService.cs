using Authentication.Domain.DTOs;

namespace Authentication.Application.Interfaces;
public interface ITokenService
{
    string GetToken(UserDTO user);
}
