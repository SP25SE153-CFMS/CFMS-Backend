using CFMS.Application.Common;
using CFMS.Application.Features.FoodFeat.Update;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.HarvestProductFeat.Update
{
    public class UpdateHarvestProductCommandHandler : IRequestHandler<UpdateHarvestProductCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateHarvestProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(UpdateHarvestProductCommand request, CancellationToken cancellationToken)
        {
            var existHarvest = _unitOfWork.HarvestProductRepository.Get(filter: f => f.HarvestProductId.Equals(request.HarvestProductId) && f.IsDeleted == false).FirstOrDefault();
            if (existHarvest == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Sản phẩm thu hoạch không tồn tại");
            }

            //var existNameCode = _unitOfWork.FoodRepository.Get(filter: s => s.FoodCode.Equals(request.FoodCode) && s.IsDeleted == false && s.FoodId != request.HarvestProductId).FirstOrDefault();
            //if (existNameCode != null)
            //{
            //    return BaseResponse<bool>.SuccessResponse("Mã thực phẩm đã tồn tại");
            //}

            try
            {
                existHarvest.HarvestProductName = request.HarvestProductName;
                existHarvest.HarvestProductCode = request.HarvestProductCode;
                existHarvest.HarvestProductTypeId = request.HarvestProductTypeId;

                _unitOfWork.HarvestProductRepository.Update(existHarvest);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Cập nhật thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Cập nhật không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra:" + ex.Message);
            }
        }
    }
}
