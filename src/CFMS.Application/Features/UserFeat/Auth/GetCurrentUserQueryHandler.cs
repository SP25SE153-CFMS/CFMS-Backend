using CFMS.Application.Common;
using CFMS.Application.DTOs.Auth;
using CFMS.Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.UserFeat.Auth
{
    public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, BaseResponse<CurrentUserResponse>>
    {
        private readonly ICurrentUserService _currentUserService;

        public GetCurrentUserQueryHandler(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public async Task<BaseResponse<CurrentUserResponse>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var currentUser = new CurrentUserResponse
            {
                UserId = _currentUserService.GetUserId(),
                Email = _currentUserService.GetUserEmail(),
                Role = _currentUserService.GetUserRole()
            };

            if (currentUser == null)
            {
                return BaseResponse<CurrentUserResponse>.FailureResponse("Người dùng không hợp lệ");
            }

            return BaseResponse<CurrentUserResponse>.SuccessResponse(currentUser);
        }
    }
}
