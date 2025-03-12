using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.UserFeat.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, BaseResponse<User>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<User>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var existUser = _unitOfWork.UserRepository.GetByID(request.UserId);
            if (existUser == null)
            {
                return BaseResponse<User>.FailureResponse("Người dùng không tồn tại");
            }
            return BaseResponse<User>.SuccessResponse(existUser);
        }
    }
}
