using CFMS.Application.Common;
using CFMS.Application.DTOs.Auth;
using CFMS.Application.Services;
using CFMS.Domain.Entities;
using CFMS.Domain.Enums.Roles;
using CFMS.Domain.Enums.Status;
using CFMS.Domain.Interfaces;
using Google.Apis.Auth;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.UserFeat.Auth
{
    public class SignInWithGoogleCommandHandler : IRequestHandler<SignInWithGoogleCommand, BaseResponse<AuthResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUtilityService _utilityService;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public SignInWithGoogleCommandHandler(IUnitOfWork unitOfWork, ITokenService tokenService, IUtilityService utilityService, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _utilityService = utilityService;
            _configuration = configuration;
        }

        public async Task<BaseResponse<AuthResponse>> Handle(SignInWithGoogleCommand request, CancellationToken cancellationToken)
        {
            var googleClientId = _configuration["Authentication:Google:ClientId"];
            var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken,
                new GoogleJsonWebSignature.ValidationSettings { Audience = new[] { googleClientId } });

            if (payload == null)
                throw new UnauthorizedAccessException("Invalid Google Token");

            var user = _unitOfWork.UserRepository.Get(filter: u => u.Mail.Equals(payload.Email)).FirstOrDefault();

            if (user == null)
            {
                user = new User
                {
                    Mail = payload.Email,
                    FullName = payload.Name,
                    Avatar = payload.Picture,
                    Status = (int)UserStatus.Active,
                    SystemRole = (int)SystemRole.User,
                };

                _unitOfWork.UserRepository.Insert(user);
                await _unitOfWork.SaveChangesAsync();
            }

            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken(user);

            var authResponse = new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            return BaseResponse<AuthResponse>.SuccessResponse(authResponse, "Đăng nhập thành công");
        }
    }
}
