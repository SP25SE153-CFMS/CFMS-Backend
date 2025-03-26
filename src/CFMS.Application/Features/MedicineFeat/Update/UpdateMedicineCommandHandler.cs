using CFMS.Application.Common;
using CFMS.Application.Features.FoodFeat.Update;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.MedicineFeat.Update
{
    public class UpdateMedicineCommandHandler : IRequestHandler<UpdateMedicineCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateMedicineCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(UpdateMedicineCommand request, CancellationToken cancellationToken)
        {
            var existMedicine = _unitOfWork.MedicineRepository.GetByID(request.MedicineId);
            if (existMedicine == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Dược phẩm không tồn tại");
            }

            try
            {
                existMedicine.MedicineName = request.MedicineName;
                existMedicine.MedicineCode = request.MedicineCode;
                existMedicine.Usage = request.Usage;
                existMedicine.DosageForm = request.DosageForm;
                existMedicine.StorageCondition = request.StorageCondition;
                existMedicine.DiseaseId = request.DiseaseId;
                existMedicine.ProductionDate = request.ProductionDate;
                existMedicine.ExpiryDate = request.ExpiryDate;

                _unitOfWork.MedicineRepository.Update(existMedicine);
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
