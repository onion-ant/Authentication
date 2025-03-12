using Authentication.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Authentication.Application.Requests;
public class CreateUserRequest
{
    [Required]
    public string Name { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    public User ToUser(string passwordHash)
    {
        return new User()
        {
            Name = Name,
            Email = Email,
            PasswordHash = passwordHash
        };
    }
}
