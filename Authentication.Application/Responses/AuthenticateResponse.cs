using Authentication.Domain.DTOs;

namespace Authentication.Application.Responses;
public class AuthenticateResponse
{
    public string Token { get; set; }
    public UserDTO User { get; set; }
}
