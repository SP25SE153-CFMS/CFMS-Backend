using CFMS.Application.Common;
using CFMS.Application.Features.FoodFeat.Update;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.EquipmentFeat.Update
{
    public class UpdateEquipmentCommandHandler : IRequestHandler<UpdateEquipmentCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEquipmentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(UpdateEquipmentCommand request, CancellationToken cancellationToken)
        {
            var existEquipment = _unitOfWork.EquipmentRepository.Get(filter: e => e.EquipmentId.Equals(request.EquipmentId) && e.IsDeleted == false).FirstOrDefault();
            if (existEquipment == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Trang thiết bị không tồn tại");
            }

            var existNameCode = _unitOfWork.EquipmentRepository.Get(filter: s => s.EquipmentCode.Equals(request.EquipmentCode) || s.EquipmentName.Equals(request.EquipmentName) && s.IsDeleted == false && s.EquipmentId != request.EquipmentId).FirstOrDefault();
            if (existEquipment != null)
            {
                return BaseResponse<bool>.FailureResponse("Tên hoặc mã trang thiết bị đã tồn tại");
            }

            var existSizeUnit = _unitOfWork.SubCategoryRepository.Get(filter: s => s.SubCategoryId.Equals(request.SizeUnitId) && s.IsDeleted == false).FirstOrDefault();
            if (existEquipment == null)
            {
                return BaseResponse<bool>.FailureResponse("Đơn vị đo kích cỡ không tồn tại");
            }

            var existWeightUnit = _unitOfWork.SubCategoryRepository.Get(filter: s => s.SubCategoryId.Equals(request.WeightUnitId) && s.IsDeleted == false).FirstOrDefault();
            if (existEquipment == null)
            {
                return BaseResponse<bool>.FailureResponse("Đơn vị đo khối lượng không tồn tại");
            }

            try
            {
                existEquipment.EquipmentName = request.EquipmentName;
                existEquipment.EquipmentCode = request.EquipmentCode;
                existEquipment.MaterialId = request.MaterialId;
                existEquipment.Usage = request.Usage;
                existEquipment.Warranty = request.Warranty;
                existEquipment.Size = request.Size;
                existEquipment.SizeUnitId = request.SizeUnitId;
                existEquipment.Weight = request.Weight;
                existEquipment.WeightUnitId = request.WeightUnitId;
                existEquipment.PurchaseDate = request.PurchaseDate;

                _unitOfWork.EquipmentRepository.Update(existEquipment);
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
