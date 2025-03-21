﻿using Authentication.Application.Interfaces;
using Authentication.Domain.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Authentication.Application.Services;
public class TokenService(IConfiguration configuration) : ITokenService
{
    private readonly IConfiguration _configuration = configuration;

    public string GetToken(UserDTO user)
    {
        var idClaim = new Claim(ClaimTypes.NameIdentifier, user.Id.ToString());
        var roleClaim = new Claim(ClaimTypes.Role, user.Role.ToString());
        var claims = new List<Claim>() { idClaim, roleClaim };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]!));

        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var tokenDescriptor = new JwtSecurityToken(
            issuer: _configuration["Token:Issuer"],
            audience: _configuration["Token:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: cred
            );

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
    public string GetRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}
