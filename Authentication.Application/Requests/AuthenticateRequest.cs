﻿using System.ComponentModel.DataAnnotations;

namespace Authentication.Application.Requests;
public class AuthenticateRequest
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}
