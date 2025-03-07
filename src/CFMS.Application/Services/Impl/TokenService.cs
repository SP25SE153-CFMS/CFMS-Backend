//using CFMS.Domain.Entities;
//using Microsoft.IdentityModel.Tokens;
//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq;
//using System.Security.Claims;
//using System.Security.Cryptography;
//using System.Text;
//using System.Threading.Tasks;

//namespace CFMS.Application.Services.Impl
//{
//    public class TokenService : ITokenService
//    {
//        private readonly string _accessTokenSecret;
//        private readonly string _refreshTokenSecret;
//        private readonly string _issuer;
//        private readonly string _audience;

//        public TokenService(string accessTokenSecret, string refreshTokenSecret, string issuer, string audience)
//        {
//            _accessTokenSecret = accessTokenSecret;
//            _refreshTokenSecret = refreshTokenSecret;
//            _issuer = issuer;
//            _audience = audience;
//        }

//        public string GenerateAccessToken(User user)
//        {
//            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_accessTokenSecret));
//            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

//            var claims = new List<Claim>
//            {
//                new Claim(ClaimTypes.Name, user.Fullname),
//                new Claim(ClaimTypes.NameIdentifier, user.Userid.ToString())
//            };

//            var tokenDescriptor = new SecurityTokenDescriptor
//            {
//                Subject = new ClaimsIdentity(claims),
//                Expires = DateTime.UtcNow.AddMinutes(15),
//                SigningCredentials = credentials,
//                Issuer = _issuer,
//                Audience = _audience
//            };

//            var tokenHandler = new JwtSecurityTokenHandler();
//            var token = tokenHandler.CreateToken(tokenDescriptor);
//            return tokenHandler.WriteToken(token);
//        }

//        public string GenerateRefreshToken()
//        {
//            var randomBytes = new byte[32];
//            using (var rng = RandomNumberGenerator.Create())
//            {
//                rng.GetBytes(randomBytes);
//            }
//            return Convert.ToBase64String(randomBytes);
//        }

//        public string RefreshAccessToken(string refreshToken)
//        {
//            var user = GetUserByRefreshToken(refreshToken);
//            if (user == null)
//            {
//                throw new UnauthorizedAccessException("Invalid refresh token.");
//            }

//            return GenerateAccessToken(user);
//        }

//        private User GetUserByRefreshToken(string refreshToken)
//        {
//            return new User();
//        }
//    }
//}
