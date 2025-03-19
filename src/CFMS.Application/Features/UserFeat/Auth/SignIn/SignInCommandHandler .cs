using CFMS.Application.Common;
using CFMS.Application.DTOs.Auth;
using CFMS.Application.Services;
using CFMS.Domain.Entities;
using CFMS.Domain.Enums.Types;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.UserFeat.Auth.SignIn
{
    public class SignInCommandHandler : IRequestHandler<SignInCommand, BaseResponse<AuthResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUtilityService _utilityService;
        private readonly ITokenService _tokenService;

        public SignInCommandHandler(IUnitOfWork unitOfWork, ITokenService tokenService, IUtilityService utilityService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _utilityService = utilityService;
        }

        public async Task<BaseResponse<AuthResponse>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var user = _unitOfWork.UserRepository.Get(filter: x => x.Mail == request.Mail)
                .FirstOrDefault();

            if (user == null)
            {
                return BaseResponse<AuthResponse>.FailureResponse("Người dùng không tồn tại");
            }

            bool isPasswordValid = _utilityService.VerifyPassword(request.Password, user.HashedPassword);
            if (!isPasswordValid)
            {
                return BaseResponse<AuthResponse>.FailureResponse("Mail hoặc mật khẩu không hợp lệ");
            }

            var accessToken = _tokenService.GenerateAccessToken(user);
            var revokedToken = _unitOfWork.RevokedTokenRepository.Get(
                filter: x => x.UserId == user.UserId
                && x.RevokedAt == null)
                .FirstOrDefault();

            if (revokedToken == null)
            {
                var refreshToken = _tokenService.GenerateRefreshToken(user);
                revokedToken = new RevokedToken
                {
                    Token = refreshToken,
                    TokenType = (int)TokenType.RefreshToken,
                    UserId = user.UserId,
                    ExpiryDate = _utilityService.ToVietnamTime(_tokenService.GetExpiryDate(refreshToken) ?? default)
                };
                _unitOfWork.RevokedTokenRepository.Insert(revokedToken);
                await _unitOfWork.SaveChangesAsync();
            }

            var authResponse = new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = revokedToken.Token
            };

            return BaseResponse<AuthResponse>.SuccessResponse(authResponse, "Đăng nhập thành công");
        }

    }
}
