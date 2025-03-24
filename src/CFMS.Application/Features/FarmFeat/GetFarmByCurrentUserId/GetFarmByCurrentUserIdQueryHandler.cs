using CFMS.Application.Common;
using CFMS.Application.Features.FarmFeat.GetFarmByUserId;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.FarmFeat.GetFarmByCurrentUserId
{
    public class GetFarmByCurrentUserIdQueryHandler : IRequestHandler<GetFarmByCurrentUserIdQuery, BaseResponse<IEnumerable<Farm>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public GetFarmByCurrentUserIdQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<BaseResponse<IEnumerable<Farm>>> Handle(GetFarmByCurrentUserIdQuery request, CancellationToken cancellationToken)
        {
            var currentUser = _currentUserService.GetUserId();
            var existFarm = _unitOfWork.FarmRepository.Get(filter: f => f.CreatedByUserId.ToString().Equals(currentUser) && f.IsDeleted == false, includeProperties: "BreedingAreas").ToList();
            if (existFarm == null)  
            {
                return BaseResponse<IEnumerable<Farm>>.FailureResponse(message: "Trang trại không tồn tại");
            }

            return BaseResponse<IEnumerable<Farm>>.SuccessResponse(data: existFarm);
        }
    }
}
