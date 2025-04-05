using CFMS.Application.Common;
using CFMS.Application.DTOs.Auth;
using CFMS.Application.Features.UserFeat.Auth.SignUp;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.UserFeat.Auth.VerifyPassword
{
    public class VerifyPasswordQueryHandlerr : IRequestHandler<VerifyPasswordQuery, BaseResponse<bool>>
    {
        private readonly IUtilityService _utilityService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;

        public VerifyPasswordQueryHandlerr(IUtilityService utilityService, ICurrentUserService currentUserService, IUnitOfWork unitOfWork)
        {
            _utilityService = utilityService;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(VerifyPasswordQuery request, CancellationToken cancellationToken)
        {
            var user = _currentUserService.GetUserId;

            var userHashedPassword = _unitOfWork.UserRepository.Get(filter: u => u.UserId.Equals(user)).FirstOrDefault().HashedPassword;

            var isMatch = _utilityService.VerifyPassword(request.Password, userHashedPassword);

            return BaseResponse<bool>.SuccessResponse(isMatch, "Xác thực mật khẩu thành công");
        }
    }
}
