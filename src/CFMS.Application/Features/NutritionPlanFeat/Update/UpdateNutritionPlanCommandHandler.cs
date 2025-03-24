using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.Features.NutritionPlanFeat.Update;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

public class UpdateNutritionPlanCommandHandler : IRequestHandler<UpdateNutritionPlanCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateNutritionPlanCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<bool>> Handle(UpdateNutritionPlanCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var nutritionPlan = _unitOfWork.NutritionPlanRepository
                .Get(filter: p => p.NutritionPlanId == request.NutritionPlanId && p.IsDeleted == false)
                .FirstOrDefault();

            if (nutritionPlan == null)
            {
                return BaseResponse<bool>.FailureResponse($"Chế độ dinh dưỡng không tồn tại");
            }

            nutritionPlan.Name = request.Name ?? nutritionPlan.Name;
            nutritionPlan.Description = request.Description ?? nutritionPlan.Description;

            _unitOfWork.NutritionPlanRepository.Update(nutritionPlan);
            await _unitOfWork.SaveChangesAsync();

            if (request.ChickenList != null)
            {
                var chickens = _unitOfWork.ChickenRepository
                    .Get(filter: c => request.ChickenList.Contains(c.ChickenId))
                    .ToList();

                var missingIds = request.ChickenList.Except(chickens.Select(c => c.ChickenId)).ToList();
                if (missingIds.Any())
                {
                    return BaseResponse<bool>.FailureResponse($"Một số loại gà không tồn tại trong hệ thống");
                }

                var existingChickenNutritions = _unitOfWork.ChickenNutritionRepository
                    .Get(filter: cn => cn.NutritionPlanId == request.NutritionPlanId)
                    .ToList();

                _unitOfWork.ChickenNutritionRepository.DeleteRange(existingChickenNutritions);
                await _unitOfWork.SaveChangesAsync();

                var newChickenNutritions = chickens.Select(chicken => new ChickenNutrition
                {
                    NutritionPlanId = nutritionPlan.NutritionPlanId,
                    ChickenId = chicken.ChickenId
                }).ToList();

                _unitOfWork.ChickenNutritionRepository.InsertRange(newChickenNutritions);
                await _unitOfWork.SaveChangesAsync();
            }

            if (request.NutritionPlanDetails != null)
            {
                var existingDetails = _unitOfWork.NutritionPlanDetailRepository
                    .Get(filter: d => d.NutritionPlanId == request.NutritionPlanId)
                    .ToList();

                _unitOfWork.NutritionPlanDetailRepository.DeleteRange(existingDetails);
                await _unitOfWork.SaveChangesAsync();

                var newDetails = request.NutritionPlanDetails.Select(detail => new NutritionPlanDetail
                {
                    NutritionPlanId = nutritionPlan.NutritionPlanId,
                    FoodId = detail.FoodId,
                    UnitId = detail.UnitId,
                    FoodWeight = detail.FoodWeight
                }).ToList();

                _unitOfWork.NutritionPlanDetailRepository.InsertRange(newDetails);
                await _unitOfWork.SaveChangesAsync();
            }

            return BaseResponse<bool>.SuccessResponse(message: "Cập nhật thành công");
        }
        catch (Exception ex)
        {
            return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra:" + ex.Message);
        }
    }

}
