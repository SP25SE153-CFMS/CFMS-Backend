using CFMS.Application.Common;
using CFMS.Application.Features.SupplierFeat.Update;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.FoodFeat.Update
{
    public class UpdateFoodCommandHandler : IRequestHandler<UpdateFoodCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateFoodCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(UpdateFoodCommand request, CancellationToken cancellationToken)
        {
            var existFood = _unitOfWork.FoodRepository.Get(filter: f => f.FoodId.Equals(request.FoodId) && f.IsDeleted == false).FirstOrDefault();
            if (existFood == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Thực phẩm không tồn tại");
            }

            var existNameCode = _unitOfWork.FoodRepository.Get(filter: s => s.FoodCode.Equals(request.FoodCode) || s.FoodName.Equals(request.FoodName) && s.IsDeleted == false).FirstOrDefault();
            if (existFood != null)
            {
                return BaseResponse<bool>.FailureResponse("Tên hoặc mã thực phẩm đã tồn tại");
            }

            try
            {
                existFood.FoodName = request.FoodName;
                existFood.FoodCode = request.FoodCode;
                existFood.Note = request.Note;
                existFood.ProductionDate = request.ProductionDate;
                existFood.ExpiryDate = request.ExpiryDate;

                _unitOfWork.FoodRepository.Update(existFood);
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
