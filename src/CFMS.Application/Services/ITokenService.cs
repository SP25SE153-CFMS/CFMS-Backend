using CFMS.Application.DTOs.Auth;
using CFMS.Domain.Entities;

namespace CFMS.Application.Services
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken(User user);
        AuthResponse RefreshAccessToken(string refreshToken);
    }
}
