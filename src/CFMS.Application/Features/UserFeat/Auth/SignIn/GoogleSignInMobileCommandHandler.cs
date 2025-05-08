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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.UserFeat.Auth.SignIn
{
    public class GoogleSignInMobileCommandHandler : IRequestHandler<GoogleSignInMobileCommand, BaseResponse<AuthResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUtilityService _utilityService;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public GoogleSignInMobileCommandHandler(IUnitOfWork unitOfWork, ITokenService tokenService, IUtilityService utilityService, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _utilityService = utilityService;
            _configuration = configuration;
        }

        public async Task<BaseResponse<AuthResponse>> Handle(GoogleSignInMobileCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.IdToken))
                return BaseResponse<AuthResponse>.FailureResponse("Thiếu idToken" );

            var clientId = _configuration["Authentication:Google:AndroidClientId"];

            GoogleJsonWebSignature.Payload payload;
            try
            {
                payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken,
                    new GoogleJsonWebSignature.ValidationSettings
                    {
                        Audience = new[] { clientId }
                    });
            }
            catch
            {
                return BaseResponse<AuthResponse>.FailureResponse("Token không hợp lệ");
            }

            var user = _unitOfWork.UserRepository.Get(filter: u => u.GoogleId == payload.Subject).FirstOrDefault();

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
            }

            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken(user);

            var revoked = new RevokedToken
            {
                Token = refreshToken,
                TokenType = (int)TokenType.REFRESH_TOKEN,
                UserId = user.UserId,
                ExpiryDate = _tokenService.GetExpiryDate(refreshToken)
            };
            _unitOfWork.RevokedTokenRepository.Insert(revoked);
            await _unitOfWork.SaveChangesAsync();

            var authResponse = new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = revoked.Token
            };

            return BaseResponse<AuthResponse>.SuccessResponse(authResponse, "Đăng nhập thành công");
        }
    }
}
