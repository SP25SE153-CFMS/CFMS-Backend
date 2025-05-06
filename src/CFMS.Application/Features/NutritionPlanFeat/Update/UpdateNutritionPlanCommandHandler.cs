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
        var existNutritionPlan = _unitOfWork.NutritionPlanRepository.Get(filter: np => np.NutritionPlanId.Equals(request.NutritionPlanId) && np.IsDeleted == false).FirstOrDefault();
        if (existNutritionPlan == null)
        {
            return BaseResponse<bool>.FailureResponse(message: "Chế độ dinh dưỡng không tồn tại");
        }

        var existName = _unitOfWork.NutritionPlanRepository.Get(filter: p => p.Name.Equals(request.Name) && p.IsDeleted == false && p.FarmId.Equals(existNutritionPlan.FarmId) && p.NutritionPlanId != request.NutritionPlanId).FirstOrDefault();
        if (existName != null)
        {
            return BaseResponse<bool>.FailureResponse("Tên chế độ dinh dưỡng đã tồn tại");
        }

        try
        {
            existNutritionPlan.Description = request.Description;
            existNutritionPlan.Name = request.Name;

            foreach (var nutritionPlanDetail in request.NutritionPlanDetails)
            {
                var existNutritionPlanDetail = _unitOfWork.NutritionPlanDetailRepository.Get(filter: npd => npd.NutritionPlanId.Equals(existNutritionPlan.NutritionPlanId) && npd.NutritionPlanDetailId.Equals(nutritionPlanDetail.NutritionPlanDetailId)).FirstOrDefault();
                if (existNutritionPlanDetail == null)
                {
                    return BaseResponse<bool>.FailureResponse(message: "Chi tiết không tồn tại");
                }

                var existFood = _unitOfWork.FoodRepository.Get(filter: f => f.FoodId.Equals(nutritionPlanDetail.FoodId)).FirstOrDefault();
                if (existFood == null)
                {
                    return BaseResponse<bool>.FailureResponse(message: "Thức ăn không tồn tại");
                }

                existNutritionPlanDetail.FoodId = nutritionPlanDetail.FoodId;
                existNutritionPlanDetail.FoodWeight = nutritionPlanDetail.FoodWeight;
                existNutritionPlanDetail.UnitId = nutritionPlanDetail.UnitId;

                _unitOfWork.NutritionPlanDetailRepository.Update(existNutritionPlanDetail);
            }

            foreach (var feedSession in request.FeedSessions)
            {
                var existFeedSession = _unitOfWork.FeedSessionRepository.Get(f => f.NutritionPlanId.Equals(existNutritionPlan.NutritionPlanId) && f.FeedSessionId.Equals(feedSession.FeedSessionId) && f.IsDeleted == false).FirstOrDefault();
                if (existFeedSession == null)
                {
                    return BaseResponse<bool>.FailureResponse(message: "Cữ cho ăn không tồn tại");
                }

                existFeedSession.FeedAmount = feedSession.FeedAmount;
                existFeedSession.StartTime = feedSession.StartTime;
                existFeedSession.EndTime = feedSession.EndTime;
                existFeedSession.Note = feedSession.Note;
                existFeedSession.UnitId = feedSession.UnitId;

                _unitOfWork.FeedSessionRepository.Update(existFeedSession);
            }

            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0
                ? BaseResponse<bool>.SuccessResponse(message: "Tạo thành công")
                : BaseResponse<bool>.FailureResponse(message: "Tạo không thành công");
        }
        catch (Exception ex)
        {
            return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra:" + ex.Message);
        }
    }

}
