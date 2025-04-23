using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.GrowthStageFeat.DeleteNutritionPlan
{
    public class DeleteGrowthStageNutritionPlanCommandHandler : IRequestHandler<DeleteGrowthStageNutritionPlanCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteGrowthStageNutritionPlanCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteGrowthStageNutritionPlanCommand request, CancellationToken cancellationToken)
        {
            var existGrowthStage = _unitOfWork.GrowthStageRepository.Get(filter: s => s.GrowthStageId.Equals(request.GrowthStageId) && s.IsDeleted == false).FirstOrDefault();
            if (existGrowthStage == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Giai đoạn phát triển không tồn tại");
            }

            var existNutritionPlan = _unitOfWork.NutritionPlanRepository.Get(filter: n => n.NutritionPlanId.Equals(request.NutritionPlanId) && n.IsDeleted == false).FirstOrDefault();
            if (existNutritionPlan == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Chế độ dinh dưỡng không tồn tại");
            }

            try
            {
                existNutritionPlan.GrowthStages.Remove(existGrowthStage);

                _unitOfWork.NutritionPlanRepository.Update(existNutritionPlan);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Xóa thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Xóa không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra:" + ex.Message);
            }
        }
    }
}
