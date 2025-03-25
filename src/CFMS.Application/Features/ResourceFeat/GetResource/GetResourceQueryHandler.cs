using CFMS.Application.Common;
using CFMS.Application.Features.SupplierFeat.GetSupplier;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.ResourceFeat.GetResource
{
    public class GetResourceQueryHandler : IRequestHandler<GetResourceQuery, BaseResponse<Resource>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUtilityService _utilityService;

        public GetResourceQueryHandler(IUnitOfWork unitOfWork, IUtilityService utilityService)
        {
            _unitOfWork = unitOfWork;
            _utilityService = utilityService;
        }

        public async Task<BaseResponse<Resource>> Handle(GetResourceQuery request, CancellationToken cancellationToken)
        { 
            var existResource = _unitOfWork.ResourceRepository.Get(filter: f => f.ResourceId.Equals(request.Id) && f.IsDeleted == false, includeProperties: "Food,Equipment,Medicine").FirstOrDefault();
            if (existResource == null)
            {
                return BaseResponse<Resource>.FailureResponse(message: "Hàng hoá không tồn tại");
            }

            return BaseResponse<Resource>.SuccessResponse(data: existResource);
        }
    }
}
