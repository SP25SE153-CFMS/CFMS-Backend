using CFMS.Application.DTOs.Auth;
using CFMS.Application.Services;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
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
            new Claim(ClaimTypes.Role, user.RoleName.ToString().ToUpper()),
            new Claim("IsRevoked", "false")
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

    public string GenerateRefreshToken(User user)
    {
        var key = new SymmetricSecurityKey(Convert.FromBase64String(_refreshSecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Role, user.RoleName.ToString().ToUpper()),
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

    public AuthResponse RefreshAccessToken(string refreshToken)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(Convert.FromBase64String(_refreshSecretKey));

        try
        {
            var principal = handler.ValidateToken(refreshToken, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidIssuer = _issuer,
                ValidAudience = _audience,
                IssuerSigningKey = key
            }, out SecurityToken validatedToken);

            var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier);
            var roleClaim = principal.FindFirst(ClaimTypes.Role);

            if (userIdClaim == null || roleClaim == null) return null;

            var user = new User
            {
                UserId = Guid.Parse(userIdClaim.Value.ToString()),
                RoleName = roleClaim.Value.ToString().ToUpper()
            };

            RevokeRefreshToken(refreshToken);

            return new AuthResponse
            {
                AccessToken = GenerateAccessToken(user),
                RefreshToken = GenerateRefreshToken(user)
            };
        }
        catch
        {
            return null;
        }
    }

    public bool RevokeRefreshToken(string refreshToken)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(Convert.FromBase64String(_refreshSecretKey));
        try
        {
            var principal = handler.ValidateToken(refreshToken, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidIssuer = _issuer,
                ValidAudience = _audience,
                IssuerSigningKey = key
            }, out SecurityToken validatedToken);
            var isRevokedClaim = principal.FindFirst("IsRevoked");
            if (isRevokedClaim == null) return false;
            return bool.Parse(isRevokedClaim.Value);
        }
        catch
        {
            return false;
        }
    }
}
