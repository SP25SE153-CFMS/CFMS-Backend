using CFMS.Application.Common;
using CFMS.Application.Features.FarmFeat.GetFarms;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.UserFeat.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, BaseResponse<IEnumerable<User>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUsersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<IEnumerable<User>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = _unitOfWork.UserRepository.Get();
            return BaseResponse<IEnumerable<User>>.SuccessResponse(users);
        }
    }
}
