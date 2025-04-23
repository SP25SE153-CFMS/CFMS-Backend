using CFMS.Application.Common;
using CFMS.Application.Features.SupplierFeat.Delete;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.FoodFeat.Delete
{
    public class DeleteFoodCommandHandler : IRequestHandler<DeleteFoodCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteFoodCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteFoodCommand request, CancellationToken cancellationToken)
        {
            var existFood = _unitOfWork.FoodRepository.Get(filter: f => f.FoodId.Equals(request.Id) && f.IsDeleted == false).FirstOrDefault();
            if (existFood == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Thực phẩm không tồn tại");
            }

            try
            {
                _unitOfWork.FoodRepository.Delete(existFood);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Xóa thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Xoá không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra:" + ex.Message);
            }
        }
    }
}
