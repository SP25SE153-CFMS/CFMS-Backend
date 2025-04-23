using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.GrowthStageFeat.UpdateNutritionPlan
{
    public class UpdateGrowthStageNutritionPlanCommandHandler : IRequestHandler<UpdateGrowthStageNutritionPlanCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateGrowthStageNutritionPlanCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(UpdateGrowthStageNutritionPlanCommand request, CancellationToken cancellationToken)
        {
            var existNutritionPlan = _unitOfWork.NutritionPlanRepository.Get(filter: n => n.NutritionPlanId.Equals(request.NutritionPlanId) && n.IsDeleted == false).FirstOrDefault();
            if (existNutritionPlan == null)
            {
                return BaseResponse<bool>.SuccessResponse(message: "Chế độ dinh dưỡng không tồn tại");
            }

            var existGrowthStage = _unitOfWork.GrowthStageRepository.Get(filter: g => g.GrowthStageId.Equals(request.GrowthStageId) && g.IsDeleted == false).FirstOrDefault();
            if (existGrowthStage == null)
            {
                return BaseResponse<bool>.SuccessResponse(message: "Giai đoạn phát triển không tồn tại");
            }

            try
            {
                existGrowthStage.NutritionPlanId = request.NutritionPlanId;

                _unitOfWork.GrowthStageRepository.Update(existGrowthStage);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0
                    ? BaseResponse<bool>.SuccessResponse(message: "Cập nhật thành công")
                    : BaseResponse<bool>.SuccessResponse(message: "Cập nhật ko thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra");
            }
        }
    }
}
