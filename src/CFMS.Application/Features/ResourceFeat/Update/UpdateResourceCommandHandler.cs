using CFMS.Application.Common;
using CFMS.Application.Features.FoodFeat.Update;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.ResourceFeat.Update
{
    public class UpdateResourceCommandHandler : IRequestHandler<UpdateResourceCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateResourceCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(UpdateResourceCommand request, CancellationToken cancellationToken)
        {
            var existResource = _unitOfWork.ResourceRepository.Get(filter: f => f.ResourceId.Equals(request.ResourceId)&& f.IsDeleted == false).FirstOrDefault();
            if (existResource == null)
            {
                return BaseResponse<bool>.SuccessResponse(message: "Hàng hoá không tồn tại");
            }

            if (existResource != null && existResource.FoodId == null && existResource.EquipmentId == null && existResource.MedicineId == null)
            {
                return BaseResponse<bool>.SuccessResponse(message: "Hàng hoá không tồn tại");
            }

            var existResourceType = _unitOfWork.SubCategoryRepository.Get(filter: s => s.SubCategoryId.Equals(request.ResourceTypeId) && s.IsDeleted == false).FirstOrDefault();

            if (existResourceType != null)
            {
                return BaseResponse<bool>.SuccessResponse("Loại hàng hoá không tồn tại");
            }

            var existUnit = _unitOfWork.SubCategoryRepository.Get(filter: s => s.SubCategoryId.Equals(request.UnitId) && s.IsDeleted == false).FirstOrDefault();

            if (existUnit != null)
            {
                return BaseResponse<bool>.SuccessResponse("Đơn vị đo không tồn tại");
            }

            var existPackage = _unitOfWork.SubCategoryRepository.Get(filter: s => s.SubCategoryId.Equals(request.PackageId) && s.IsDeleted == false).FirstOrDefault();

            if (existPackage != null)
            {
                return BaseResponse<bool>.SuccessResponse("Loại đóng gói không tồn tại");
            }

            try
            {
                existResource.ResourceTypeId = request.ResourceTypeId;
                //existResource.Description = request.Description;
                existResource.UnitId = request.UnitId;
                existResource.PackageId = request.PackageId;
                existResource.PackageSize = request.PackageSize;
                existResource.FoodId = request.FoodId ?? existResource.FoodId;
                existResource.EquipmentId = request.EquipmentId ?? existResource.EquipmentId;
                existResource.MedicineId = request.MedicineId ?? existResource.MedicineId;

                _unitOfWork.ResourceRepository.Update(existResource);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Cập nhật thành công");
                }
                return BaseResponse<bool>.SuccessResponse(message: "Cập nhật không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra:" + ex.Message);
            }
        }
    }
}
