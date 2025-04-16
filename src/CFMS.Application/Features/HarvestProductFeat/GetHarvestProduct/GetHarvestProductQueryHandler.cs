using CFMS.Application.Common;
using CFMS.Application.Features.FoodFeat.GetFood;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.HarvestProductFeat.GetHarvestProduct
{
    public class GetHarvestProductQueryHandler : IRequestHandler<GetHarvestProductQuery, BaseResponse<HarvestProduct>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetHarvestProductQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<HarvestProduct>> Handle(GetHarvestProductQuery request, CancellationToken cancellationToken)
        {
            var existHarvest = _unitOfWork.HarvestProductRepository.Get(filter: f => f.HarvestProductId.Equals(request.Id) && f.IsDeleted == false).FirstOrDefault();
            if (existHarvest == null)
            {
                return BaseResponse<HarvestProduct>.FailureResponse(message: "Sản phẩm thu hoạch không tồn tại");
            }

            return BaseResponse<HarvestProduct>.SuccessResponse(data: existHarvest);
        }
    }
}
