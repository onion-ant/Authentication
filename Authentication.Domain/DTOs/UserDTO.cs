using Authentication.Domain.Enums;

namespace Authentication.Domain.DTOs;
public class UserDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public ERole Role { get; set; }
}
