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
            var user = _unitOfWork.UserRepository.Get()
                .FirstOrDefault(x => x.Mail == request.Mail);

            if (user == null)
            {
                return BaseResponse<AuthResponse>.FailureResponse("User not found");
            }

            bool isPasswordValid = _utilityService.VerifyPassword(request.Password, user.HashedPassword);
            if (!isPasswordValid)
            {
                return BaseResponse<AuthResponse>.FailureResponse("Invalid email or password");
            }

            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            var authResponse = new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            return BaseResponse<AuthResponse>.SuccessResponse(authResponse, "Login successful");
        }

    }
}
