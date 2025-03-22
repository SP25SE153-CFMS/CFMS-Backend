using CFMS.Application.Common;
using CFMS.Application.Features.NutritionPlanFeat.Update;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

public class UpdateNutritionPlanCommandHandler : IRequestHandler<UpdateNutritionPlanCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateNutritionPlanCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResponse<bool>> Handle(UpdateNutritionPlanCommand request, CancellationToken cancellationToken)
    {
        var existPlan = _unitOfWork.NutritionPlanRepository
            .Get(filter: p => p.NutritionPlanId.Equals(request.NutritionPlanId) && p.IsDeleted == false)
            .FirstOrDefault();

        if (existPlan == null)
        {
            return BaseResponse<bool>.FailureResponse(message: "Chế độ dinh dưỡng không tồn tại");
        }

        try
        {
            existPlan.Description = request.Description;
            existPlan.Name = request.Name;

            if (request.ChickenList != null && request.ChickenList.Any())
            {
                var chickens = _unitOfWork.ChickenRepository
                    .Get(c => request.ChickenList.Contains(c.ChickenId))
                    .ToList();

                existPlan.Chickens.Clear();
                existPlan.Chickens = chickens;
            }

            var existingDetails = _unitOfWork.NutritionPlanDetailRepository
                .Get(filter: d => d.NutritionPlanId == existPlan.NutritionPlanId)
                .ToList();

            if (existingDetails.Any())
            {
                _unitOfWork.NutritionPlanDetailRepository.DeleteRange(existingDetails);
            }

            if (request.NutritionPlanDetails != null && request.NutritionPlanDetails.Any())
            {
                var newDetails = request.NutritionPlanDetails.Select(detail => new NutritionPlanDetail
                {
                    NutritionPlanId = existPlan.NutritionPlanId,
                    FoodId = detail.FoodId,
                    UnitId = detail.UnitId,
                    FoodWeight = detail.FoodWeight,
                    ConsumptionRate = detail.ConsumptionRate
                }).ToList();

                _unitOfWork.NutritionPlanDetailRepository.InsertRange(newDetails);
            }

            _unitOfWork.NutritionPlanRepository.Update(existPlan);
            var result = await _unitOfWork.SaveChangesAsync();

            if (result > 0)
            {
                return BaseResponse<bool>.SuccessResponse(message: "Cập nhật thành công");
            }
            return BaseResponse<bool>.FailureResponse(message: "Cập nhật không thành công");
        }
        catch (Exception ex)
        {
            return BaseResponse<bool>.FailureResponse(message: $"Có lỗi xảy ra: {ex.Message}");
        }
    }
}
