using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.NutritionPlanFeat.AddFood
{
    public class AddFoodCommandHandler : IRequestHandler<AddFoodCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddFoodCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(AddFoodCommand request, CancellationToken cancellationToken)
        {
            var existFood = _unitOfWork.FoodRepository.Get(filter: f => f.FoodId.Equals(request.FoodId) && f.IsDeleted == false).FirstOrDefault();
            if (existFood == null)
            {
                return BaseResponse<bool>.SuccessResponse(message: "Thức ăn không tồn tại");
            }

            var existNutritionPlan = _unitOfWork.NutritionPlanRepository.Get(filter: np => np.NutritionPlanId.Equals(request.NutritionPlanId) && np.IsDeleted == false).FirstOrDefault();
            if (existNutritionPlan == null)
            {
                return BaseResponse<bool>.SuccessResponse(message: "Chế độ dinh dưỡng không tồn tại");
            }

            try
            {
                existNutritionPlan.NutritionPlanDetails.Add(new NutritionPlanDetail
                {
                    FoodId = request.FoodId,
                    FoodWeight = request.FoodWeight,
                    UnitId = request.UnitId,
                });

                _unitOfWork.NutritionPlanRepository.Update(existNutritionPlan);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0
                   ? BaseResponse<bool>.SuccessResponse(message: "Tạo thành công")
                   : BaseResponse<bool>.SuccessResponse(message: "Tạo không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra");
            }
        }
    }
}
