using CFMS.Application.Common;
using CFMS.Application.DTOs.Auth;
using CFMS.Application.Services;
using CFMS.Domain.Entities;
using CFMS.Domain.Enums.Roles;
using CFMS.Domain.Enums.Status;
using CFMS.Domain.Enums.Types;
using CFMS.Domain.Interfaces;
using Google.Apis.Auth;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.UserFeat.Auth.SignIn
{
    public class SignInWithGoogleCommandHandler : IRequestHandler<SignInWithGoogleCommand, BaseResponse<AuthResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUtilityService _utilityService;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public SignInWithGoogleCommandHandler(IUnitOfWork unitOfWork, ITokenService tokenService, IUtilityService utilityService, IConfiguration configuration, HttpClient httpClient)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _utilityService = utilityService;
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<BaseResponse<AuthResponse>> Handle(SignInWithGoogleCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.AuthorizationCode))
                return BaseResponse<AuthResponse>.FailureResponse("Thiếu mã xác thực");

            var tokenRequest = new Dictionary<string, string>
        {
            { "code", request.AuthorizationCode },
            { "client_id", _configuration["Authentication:Google:ClientId"] },
            { "client_secret", _configuration["Authentication:Google:ClientSecret"] },
            { "redirect_uri", _configuration["Authentication:Google:RedirectUri"] },
            { "grant_type", "authorization_code" }
        };

            var tokenResponse = await _httpClient.PostAsync("https://oauth2.googleapis.com/token", new FormUrlEncodedContent(tokenRequest));
            if (!tokenResponse.IsSuccessStatusCode)
                return BaseResponse<AuthResponse>.FailureResponse("Thất bại trong việc chuyển đổi mã xác thực sang token");

            var tokenData = JsonConvert.DeserializeObject<GoogleTokenResponse>(await tokenResponse.Content.ReadAsStringAsync());

            var googleClientId = _configuration["Authentication:Google:ClientId"];
            var payload = await GoogleJsonWebSignature.ValidateAsync(tokenData.IdToken,
                new GoogleJsonWebSignature.ValidationSettings { Audience = new[] { googleClientId } });

            if (payload == null)
                throw new UnauthorizedAccessException("Token của google không hợp lệ");

            var user = _unitOfWork.UserRepository.Get(filter: u => u.GoogleId.Equals(payload.Subject)).FirstOrDefault();

            var authResponse = await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                if (user == null)
                {
                    user = new User
                    {
                        Mail = payload.Email,
                        FullName = payload.Name,
                        Avatar = payload.Picture,
                        Status = (int)UserStatus.ACTIVE_STATUS,
                        SystemRole = (int)GeneralRole.USER_ROLE,
                        GoogleId = payload.Subject
                    };

                    _unitOfWork.UserRepository.Insert(user);
                    await _unitOfWork.SaveChangesAsync();

                    var refreshToken = _tokenService.GenerateRefreshToken(user);
                    var revokedToken = new RevokedToken
                    {
                        Token = refreshToken,
                        TokenType = (int)TokenType.REFRESH_TOKEN,
                        UserId = user.UserId,
                        ExpiryDate = _utilityService.ToVietnamTime(_tokenService.GetExpiryDate(refreshToken) ?? default)
                    };

                    _unitOfWork.RevokedTokenRepository.Insert(revokedToken);
                    await _unitOfWork.SaveChangesAsync();
                }

                var accessToken = _tokenService.GenerateAccessToken(user);
                var existRevokedToken = _unitOfWork.RevokedTokenRepository.Get(
                    filter: x => x.UserId == user.UserId
                    && x.RevokedAt == null)
                    .FirstOrDefault();

                if (existRevokedToken == null)
                {
                    var refreshToken = _tokenService.GenerateRefreshToken(user);
                    existRevokedToken = new RevokedToken
                    {
                        Token = refreshToken,
                        TokenType = (int)TokenType.REFRESH_TOKEN,
                        UserId = user.UserId,
                        ExpiryDate = _utilityService.ToVietnamTime(_tokenService.GetExpiryDate(refreshToken) ?? default)
                    };
                    _unitOfWork.RevokedTokenRepository.Insert(existRevokedToken);
                    await _unitOfWork.SaveChangesAsync();
                }
                return new AuthResponse
                {
                    AccessToken = accessToken,
                    RefreshToken = existRevokedToken.Token
                };
            });

            return BaseResponse<AuthResponse>.SuccessResponse(authResponse, "Đăng nhập thành công");
        }
    }
}
