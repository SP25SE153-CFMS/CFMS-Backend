using CFMS.Application.Common;
using CFMS.Application.Features.FoodFeat.Delete;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            var existEquipment = _unitOfWork.EquipmentRepository.Get(filter: f => f.EquipmentId.Equals(request.Id) && f.IsDeleted == false).FirstOrDefault();
            if (existEquipment == null)
            {
                return BaseResponse<bool>.SuccessResponse(message: "Thực phẩm không tồn tại");

            }

            try
            {

                _unitOfWork.EquipmentRepository.Delete(existEquipment);

                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Xóa thành công");
                }

                return BaseResponse<bool>.SuccessResponse(message: "Xoá không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra:" + ex.Message);

            }
        }
    }
}
