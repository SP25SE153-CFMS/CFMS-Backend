﻿ using CFMS.Application.DTOs.Auth;
using CFMS.Application.Services;
using CFMS.Application.Services.Impl;
using CFMS.Domain.Entities;
using CFMS.Domain.Enums.Roles;
using CFMS.Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Task = System.Threading.Tasks.Task;

public class TokenService : ITokenService
{
    private readonly string _accessSecretKey;
    private readonly string _refreshSecretKey;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUtilityService _utilityService;

    public TokenService(IConfiguration config, IUnitOfWork unitOfWork, ICurrentUserService currentUserService, IUtilityService utilityService)
    {
        _accessSecretKey = config["Jwt:AccessSecretKey"];
        _refreshSecretKey = config["Jwt:RefreshSecretKey"];
        _issuer = config["Jwt:Issuer"];
        _audience = config["Jwt:Audience"];
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
        _utilityService = utilityService;
    }

    public string GenerateAccessToken(User user)
    {
        var key = new SymmetricSecurityKey(Convert.FromBase64String(_accessSecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Role, user.SystemRole.ToString().ToUpper()),
            new Claim(ClaimTypes.Email, user.Mail.ToString())
        };

        var token = new JwtSecurityToken(
            _issuer,
            _audience,
            claims,
            expires: DateTime.Now.ToLocalTime().AddHours(7).AddYears(1),
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
            new Claim(ClaimTypes.Role, user.SystemRole.ToString().ToUpper()),
            new Claim(ClaimTypes.Email, user.Mail.ToString())
        };

        var token = new JwtSecurityToken(
            _issuer,
            _audience,
            claims,
            expires: DateTime.Now.ToLocalTime().AddHours(7).AddDays(7),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<AuthResponse> RefreshAccessTokenAsync(RevokedToken token)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(Convert.FromBase64String(_refreshSecretKey));

        try
        {
            if (IsTokenExpired(token.Token) || IsTokenRevoked(token))
            {
                return null;
            }

            var principal = handler.ValidateToken(token.Token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidIssuer = _issuer,
                ValidAudience = _audience,
                IssuerSigningKey = key
            }, out SecurityToken validatedToken);

            var jwtToken = validatedToken as JwtSecurityToken;
            if (jwtToken == null) return null;

            var expiryDateUnix = long.Parse(jwtToken.Claims.First(x => x.Type == "exp").Value);
            var expiryDateTime = DateTimeOffset.FromUnixTimeSeconds(expiryDateUnix).DateTime.ToLocalTime().AddHours(7);

            var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier);
            var roleClaim = principal.FindFirst(ClaimTypes.Role);
            var mailClaim = principal.FindFirst(ClaimTypes.Email);

            if (userIdClaim == null || roleClaim == null) return null;

            var user = new User
            {
                UserId = Guid.Parse(userIdClaim.Value),
                //SystemRole = (SystemRole)Enum.Parse(typeof(SystemRole), roleClaim?.Value.ToUpper()),
                Mail = mailClaim?.Value
            };

            return await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                await RevokeRefreshTokenAsync(token);

                var newRefreshToken = GenerateRefreshToken(user);

                var newTokenEntry = new RevokedToken
                {
                    UserId = user.UserId,
                    Token = newRefreshToken,
                    RevokedAt = null,
                    ExpiryDate = GetExpiryDate(newRefreshToken)
                };

                _unitOfWork.RevokedTokenRepository.Insert(newTokenEntry);

                return new AuthResponse
                {
                    AccessToken = GenerateAccessToken(user),
                    RefreshToken = newRefreshToken
                };
            });
        }
        catch
        {
            return null;
        }
    }

    public bool IsTokenRevoked(RevokedToken token)
    {
        return token.RevokedAt != null && DateTime.Now.ToLocalTime().AddHours(7) >= token.RevokedAt;
    }

    public async Task RevokeRefreshTokenAsync(RevokedToken token)
    {
        token.RevokedAt = DateTime.Now.ToLocalTime().AddHours(7);
        _unitOfWork.RevokedTokenRepository.Update(token);
    }

    public DateTime? GetExpiryDate(string token)
    {
        var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
        var expiryDateUnix = long.Parse(jwtToken.Claims.First(x => x.Type == "exp").Value);
        var expiryDateTime = DateTimeOffset.FromUnixTimeSeconds(expiryDateUnix).DateTime.ToLocalTime().AddHours(7);

        return expiryDateTime;
    }

    public bool IsTokenExpired(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        try
        {
            var jwtToken = handler.ReadJwtToken(token);

            var expiryClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == "exp");
            if (expiryClaim == null) return true;

            var expiryDateUnix = long.Parse(expiryClaim.Value);
            var expiryDateTime = DateTimeOffset.FromUnixTimeSeconds(expiryDateUnix).DateTime.ToLocalTime().AddHours(7);

            return expiryDateTime < DateTime.Now.ToLocalTime().AddHours(7);
        }
        catch
        {
            return true;
        }
    }

    public async Task<string> GenerateJwtTokenGoogle(ClaimsPrincipal user)
    {
        var issuer = _issuer;
        var audience = _audience;
        var key = Encoding.ASCII.GetBytes(_accessSecretKey);
        var securityKey = new SymmetricSecurityKey(key);
        var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.FindFirstValue(ClaimTypes.NameIdentifier)),
            new Claim(JwtRegisteredClaimNames.Email, user.FindFirstValue(ClaimTypes.Email)),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, GeneralRole.USER_ROLE.ToString())
        };

        var identity = new ClaimsIdentity(claims);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = identity,
            Expires = DateTime.Now.ToLocalTime().AddYears(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return tokenString;
    }
}
