using Authentication.Domain.DTOs;
using Authentication.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Authentication.Domain.Entities;
public class User
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string PasswordHash { get; set; }
    [Required]
    public ERole Role { get; set; }
    [Required]
    public bool Verified { get; set; } = false;
}
