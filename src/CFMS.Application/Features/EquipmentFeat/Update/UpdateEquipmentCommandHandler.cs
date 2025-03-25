using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;

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
            var existEquip = _unitOfWork.EquipmentRepository.Get(filter: e => e.EquipmentId.Equals(request.EquipmentId) && e.IsDeleted == false).FirstOrDefault();
            if (existEquip == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Trang thiết bị không tồn tại");
            }

            try
            {
                existEquip.EquipmentCode = request.EquipmentCode;
                existEquip.EquipmentName = request.EquipmentName;
                existEquip.Material = request.Material;
                existEquip.Usage = request.Usage;
                existEquip.Warranty = request.Warranty;
                existEquip.Size = request.Size;
                existEquip.SizeUnitId = request.SizeUnitId;
                existEquip.Weight = request.Weight;
                existEquip.WeightUnitId = request.WeightUnitId;
                existEquip.PurchaseDate = request.PurchaseDate;

                _unitOfWork.EquipmentRepository.Update(existEquip);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Cập nhật thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Cập nhật ko thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra");
            }
        }
    }
}
