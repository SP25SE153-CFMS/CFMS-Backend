using CFMS.Application.Common;
using CFMS.Application.DTOs.Auth;
using CFMS.Application.Services;
using CFMS.Domain.Entities;
using CFMS.Domain.Enums.Roles;
using CFMS.Domain.Enums.Status;
using CFMS.Domain.Enums.Types;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.UserFeat.Auth
{
    public class SignUpCommandHandler : IRequestHandler<SignUpCommand, BaseResponse<AuthResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUtilityService _utilityService;
        private readonly ITokenService _tokenService;

        public SignUpCommandHandler(IUnitOfWork unitOfWork, ITokenService tokenService, IUtilityService utilityService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _utilityService = utilityService;
        }

        public async Task<BaseResponse<AuthResponse>> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            var existUser = _unitOfWork.UserRepository.Get().FirstOrDefault(x => x.Mail == request.Mail);

            if (existUser != null)
            {
                return BaseResponse<AuthResponse>.FailureResponse("Người dùng đã tồn tại");
            }

            var user = new User
            {
                FullName = request.Fullname,
                PhoneNumber = request.PhoneNumber,
                Mail = request.Mail,
                HashedPassword = _utilityService.HashPassword(request.Password),
                SystemRole = (int)SystemRole.User,
                //CreatedDate = DateOnly.FromDateTime(DateTime.Now),
                Status = (int)UserStatus.Active
            };

            var authResponse = await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                _unitOfWork.UserRepository.Insert(user);
                await _unitOfWork.SaveChangesAsync();

                var accessToken = _tokenService.GenerateAccessToken(user);
                var refreshToken = _tokenService.GenerateRefreshToken(user);

                var revokedToken = new RevokedToken
                {
                    Token = refreshToken,
                    TokenType = (int)TokenType.RefreshToken,
                    UserId = user.UserId,
                    ExpiryDate = _tokenService.GetExpiryDate(refreshToken)
                };

                _unitOfWork.RevokedTokenRepository.Insert(revokedToken);
                await _unitOfWork.SaveChangesAsync();
                return new AuthResponse
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                };
            });

            return BaseResponse<AuthResponse>.SuccessResponse(authResponse, "Đăng ký thành công");
        }
    }
}
