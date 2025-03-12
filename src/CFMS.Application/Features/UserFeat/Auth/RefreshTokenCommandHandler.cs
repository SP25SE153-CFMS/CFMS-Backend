using CFMS.Application.Common;
using CFMS.Application.DTOs.Auth;
using CFMS.Application.Features.UserFeat.Auth;
using CFMS.Application.Services;
using CFMS.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CFMS.Application.Features.UserFeat.Auth
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, BaseResponse<AuthResponse>>
    {
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;

        public RefreshTokenCommandHandler(ITokenService tokenService, IUnitOfWork unitOfWork)
        {
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<AuthResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var existToken = _unitOfWork.RevokedTokenRepository.Get(
                filter: x => x.Token.Equals(request.RefreshToken))
                .FirstOrDefault();

            if (existToken == null)
            {
                return BaseResponse<AuthResponse>.FailureResponse("Token không hợp lệ hoặc hết hạn");
            }

            var newTokens = _tokenService.RefreshAccessTokenAsync(existToken);

            if (newTokens.Result == null)
            {
                return BaseResponse<AuthResponse>.FailureResponse("Token không hợp lệ hoặc hết hạn");
            }

            return BaseResponse<AuthResponse>.SuccessResponse(newTokens.Result);
        }
    }
}