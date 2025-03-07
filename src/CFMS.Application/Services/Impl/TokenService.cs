using CFMS.Application.Services;
using CFMS.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public class TokenService : ITokenService
{
    private readonly string _accessSecretKey;
    private readonly string _refreshSecretKey;
    private readonly string _issuer;
    private readonly string _audience;

    public TokenService(IConfiguration config)
    {
        _accessSecretKey = config["Jwt:AccessSecretKey"];
        _refreshSecretKey = config["Jwt:RefreshSecretKey"];
        _issuer = config["Jwt:Issuer"];
        _audience = config["Jwt:Audience"];
    }

    public string GenerateAccessToken(User user)
    {
        var key = new SymmetricSecurityKey(Convert.FromBase64String(_accessSecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Email, user.Mail)
        };

        var token = new JwtSecurityToken(
            _issuer,
            _audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(15),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var key = new SymmetricSecurityKey(Convert.FromBase64String(_refreshSecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim("RefreshToken", Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            _issuer,
            _audience,
            claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string RefreshAccessToken(string refreshToken)
    {
        var user = GetUserByRefreshToken(refreshToken);
        if (user == null)
        {
            throw new UnauthorizedAccessException("Invalid refresh token.");
        }

        return GenerateAccessToken(user);
    }

    private User GetUserByRefreshToken(string refreshToken)
    {
        return new User();
    }
}
