using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.GrowthStageFeat.AddNutritionPlan
{
    public class AddNutritionPlanCommandHandler : IRequestHandler<AddNutritionPlanCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddNutritionPlanCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(AddNutritionPlanCommand request, CancellationToken cancellationToken)
        {
            var existGrowthStage = _unitOfWork.GrowthStageRepository.Get(filter: gt => gt.GrowthStageId.Equals(request.GrowthStageId) && gt.IsDeleted == false).FirstOrDefault();
            if (existGrowthStage == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Giai đoạn phát triển không tồn tại");
            }

            var existNutritionPlan = _unitOfWork.NutritionPlanRepository.Get(filter: np => np.NutritionPlanId.Equals(request.NutritionPlanId) && np.IsDeleted == false).FirstOrDefault();
            if (existNutritionPlan == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Chế độ dinh dưỡng không tồn tại");
            }

            try
            {
                existGrowthStage.GrowthNutritions.Add(new GrowthNutrition
                {
                    GrowthStageId = request.GrowthStageId,
                    NutritionPlanId = request.NutritionPlanId,
                });

                _unitOfWork.GrowthStageRepository.Update(existGrowthStage);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Thêm thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Thêm không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra");
            }
        }
    }
}
