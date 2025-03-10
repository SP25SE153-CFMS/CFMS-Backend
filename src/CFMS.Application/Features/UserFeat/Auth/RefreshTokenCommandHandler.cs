using CFMS.Application.Common;
using CFMS.Application.DTOs.Auth;
using CFMS.Application.Features.UserFeat.Auth;
using CFMS.Application.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CFMS.Application.Features.UserFeat.Auth
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, BaseResponse<AuthResponse>>
    {
        private readonly ITokenService _tokenService;

        public RefreshTokenCommandHandler(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task<BaseResponse<AuthResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var newTokens = _tokenService.RefreshAccessToken(request.RefreshToken);

            if (newTokens == null)
            {
                return BaseResponse<AuthResponse>.FailureResponse("Invalid or expired refresh token");
            }

            return BaseResponse<AuthResponse>.SuccessResponse(newTokens);
        }
    }
}