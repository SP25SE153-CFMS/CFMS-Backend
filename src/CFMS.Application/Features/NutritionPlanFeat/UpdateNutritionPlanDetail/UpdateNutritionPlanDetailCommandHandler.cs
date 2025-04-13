using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.NutritionPlanFeat.UpdateNutritionPlanDetail
{
    public class UpdateNutritionPlanDetailCommandHandler : IRequestHandler<UpdateNutritionPlanDetailCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateNutritionPlanDetailCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(UpdateNutritionPlanDetailCommand request, CancellationToken cancellationToken)
        {
            var existNutritionPlanDetail = _unitOfWork.NutritionPlanDetailRepository.Get(filter: n => n.NutritionPlanDetailId.Equals(request.NutritionPlanDetailId)).FirstOrDefault();
            if (existNutritionPlanDetail == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "NutritionPlanDetail không tồn tại");
            }

            var existFood = _unitOfWork.FoodRepository.Get(filter: f => f.FoodId.Equals(request.FoodId) && f.IsDeleted == false).FirstOrDefault();
            if (existFood == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Thức ăn không tồn tại");
            }

            try
            {
                existNutritionPlanDetail.FoodId = request.FoodId;
                existNutritionPlanDetail.FoodWeight = request.FoodWeight;
                existNutritionPlanDetail.UnitId = request.UnitId;

                _unitOfWork.NutritionPlanDetailRepository.Update(existNutritionPlanDetail);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Cập nhật thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Cập nhật không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra");
            }
        }
    }
}
