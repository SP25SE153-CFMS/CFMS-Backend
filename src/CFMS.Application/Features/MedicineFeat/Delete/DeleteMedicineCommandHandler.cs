using CFMS.Application.Common;
using CFMS.Application.Features.FoodFeat.Delete;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.MedicineFeat.Delete
{
    public class DeleteMedicineCommandHandler : IRequestHandler<DeleteMedicineCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMedicineCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteMedicineCommand request, CancellationToken cancellationToken)
        {
            var existMedicine = _unitOfWork.MedicineRepository.Get(filter: f => f.MedicineId.Equals(request.Id) && f.IsDeleted == false).FirstOrDefault();
            if (existMedicine == null)
            {
                return BaseResponse<bool>.SuccessResponse(message: "Thực phẩm không tồn tại");
            }

            try
            {
                _unitOfWork.MedicineRepository.Delete(existMedicine);
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
