using Authentication.Domain.Entities;
using Authentication.Domain.Enums;
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
    //Apenas para testes
    [Required]
    public ERole Role { get; set; }
}
