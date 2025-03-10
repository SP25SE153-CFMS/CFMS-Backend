using CFMS.Application.DTOs.Auth;
using CFMS.Domain.Entities;

namespace CFMS.Application.Services
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken(User user);
        Task<AuthResponse> RefreshAccessTokenAsync(RevokedToken token);
        bool IsTokenRevoked(RevokedToken token);
        DateTime? GetExpiryDate(string token);
        bool IsTokenExpired(string token);
    }
}