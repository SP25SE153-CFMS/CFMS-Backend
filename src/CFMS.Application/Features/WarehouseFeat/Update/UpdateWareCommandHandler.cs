using CFMS.Application.Common;
using CFMS.Application.Features.FoodFeat.Update;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.WarehouseFeat.Update
{
    public class UpdateWareCommandHandler : IRequestHandler<UpdateWareCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateWareCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(UpdateWareCommand request, CancellationToken cancellationToken)
        {
            var existWare = _unitOfWork.WarehouseRepository.Get(filter: f => f.WareId.Equals(request.WareId) && f.IsDeleted == false).FirstOrDefault();
            if (existWare == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Kho không tồn tại");
            }

            var existName = _unitOfWork.WarehouseRepository.Get(filter: s => s.WarehouseName.Equals(request.WarehouseName) && s.IsDeleted == false && s.WareId != request.WareId).FirstOrDefault();
            if (existWare != null)
            {
                return BaseResponse<bool>.FailureResponse("Tên kho đã tồn tại");
            }

            var existFarm = _unitOfWork.FarmRepository.Get(filter: s => s.FarmId.Equals(request.FarmId) && s.IsDeleted == false).FirstOrDefault();
            if (existFarm == null)
            {
                return BaseResponse<bool>.FailureResponse("Trang trại không tồn tại");
            }

            var existResourceType = _unitOfWork.SubCategoryRepository.Get(filter: s => s.SubCategoryId.Equals(request.ResourceTypeId) && s.IsDeleted == false).FirstOrDefault();
            if (existFarm == null)
            {
                return BaseResponse<bool>.FailureResponse("Loại hàng hoá không tồn tại");
            }

            try
            {
                existWare.FarmId = request.FarmId;
                existWare.ResourceTypeId = request.ResourceTypeId;
                existWare.WarehouseName = request.WarehouseName;
                existWare.MaxQuantity = request.MaxQuantity;
                existWare.MaxWeight = request.MaxWeight;
                existWare.CurrentQuantity = request.CurrentQuantity;
                existWare.CurrentWeight = request.CurrentWeight;
                existWare.Description = request.Description;
                existWare.Status = request.Status;

                _unitOfWork.WarehouseRepository.Update(existWare);
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
