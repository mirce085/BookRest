using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BookRest.Models;
using BookRest.Models.Enums;
using BookRest.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace BookRest.Services;

public class JwtService(IConfiguration config) : ITokenService
{
    public string GenerateAccessToken(User user)
    {
        var secretKey = config["JwtSettings:SecretKey"]!;
        var issuer = config["JwtSettings:Issuer"];
        var audience = config["JwtSettings:Audience"];

        var key = new SymmetricSecurityKey(Convert.FromBase64String(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var token = new JwtSecurityToken(
            issuer,
            audience,
            claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public RefreshToken GenerateRefreshToken() => new()
    {
        Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
        Expires = DateTime.UtcNow.AddDays(1)
    };
}