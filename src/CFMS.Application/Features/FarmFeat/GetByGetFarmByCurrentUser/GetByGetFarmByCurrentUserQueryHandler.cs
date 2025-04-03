using CFMS.Application.Common;
using CFMS.Application.Features.FarmFeat.GetFarmByCurrentUserId;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.FarmFeat.GetByGetFarmByCurrentUser
{
    public class GetByGetFarmByCurrentUserQueryHandler : IRequestHandler<GetByGetFarmByCurrentUserQuery, BaseResponse<IEnumerable<Farm>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public GetByGetFarmByCurrentUserQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<BaseResponse<IEnumerable<Farm>>> Handle(GetByGetFarmByCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var currentUser = _currentUserService.GetUserId();
            Guid userId = Guid.Parse(_currentUserService.GetUserId());

            var existFarm = _unitOfWork.FarmRepository.Get(
                  filter: f => (f.CreatedByUserId.ToString().Equals(currentUser) ||
                               f.FarmEmployees.Any(x => x.UserId.Equals(userId) && (x.FarmRole.Equals(4) || x.FarmRole.Equals(5))))
                               && f.IsDeleted == false
              ).ToList(); 
            if (existFarm == null)
            {
                return BaseResponse<IEnumerable<Farm>>.FailureResponse(message: "Trang trại không tồn tại");
            }

            return BaseResponse<IEnumerable<Farm>>.SuccessResponse(data: existFarm);
        }
    }
}
