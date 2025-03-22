using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.GrowthStageFeat.DeleteNutritionPlan
{
    public class DeleteNutritionPlanCommandHandler : IRequestHandler<DeleteNutritionPlanCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteNutritionPlanCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteNutritionPlanCommand request, CancellationToken cancellationToken)
        {
            var existGrowthStage = _unitOfWork.GrowthStageRepository.Get(filter: s => s.GrowthStageId.Equals(request.GrowthStageId) && s.IsDeleted == false).FirstOrDefault();
            if (existGrowthStage == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Giai đoạn phát triển không tồn tại");
            }

            var existGrowthNutrition = _unitOfWork.GrowthNutritionRepository.Get(filter: n => n.GrowthNutritionId.Equals(request.GrowthNutritionId) && n.IsDeleted == false).FirstOrDefault();
            if (existGrowthNutrition == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Chế độ dinh dưỡng không tồn tại");
            }

            try
            {
                existGrowthStage.GrowthNutritions.Remove(existGrowthNutrition);

                _unitOfWork.GrowthStageRepository.Update(existGrowthStage);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Xóa thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Xóa không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra");
            }
        }
    }
}
