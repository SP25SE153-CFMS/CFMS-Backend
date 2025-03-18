using CFMS.Application.Common;
using CFMS.Application.DTOs.Auth;
using CFMS.Application.Services;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.UserFeat.Auth
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
            var refreshToken = _unitOfWork.RevokedTokenRepository.Get(filter: x => x.UserId == user.UserId).FirstOrDefault().Token;

            var authResponse = new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            return BaseResponse<AuthResponse>.SuccessResponse(authResponse, "Đăng nhập thành công");
        }

    }
}
