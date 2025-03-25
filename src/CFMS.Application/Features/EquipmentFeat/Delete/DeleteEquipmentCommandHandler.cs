using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.EquipmentFeat.Delete
{
    public class DeleteEquipmentCommandHandler : IRequestHandler<DeleteEquipmentCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteEquipmentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteEquipmentCommand request, CancellationToken cancellationToken)
        {
            var existEquip = _unitOfWork.EquipmentRepository.Get(filter: e => e.EquipmentId.Equals(request.EquipmentId) && e.IsDeleted == false).FirstOrDefault();
            if (existEquip == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Trang thiết bị không tồn tại");
            }

            try
            {
                _unitOfWork.EquipmentRepository.Delete(existEquip);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Xóa thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Xóa không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra");
            }
        }
    }
}
