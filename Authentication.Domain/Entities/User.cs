﻿using System.ComponentModel.DataAnnotations;

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
    public string PasswordSalt { get; set; }
}
