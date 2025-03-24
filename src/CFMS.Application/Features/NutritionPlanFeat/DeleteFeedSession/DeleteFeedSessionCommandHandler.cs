using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.NutritionPlanFeat.DeleteFeedSession
{
    public class DeleteFeedSessionCommandHandler : IRequestHandler<DeleteFeedSessionCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteFeedSessionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteFeedSessionCommand request, CancellationToken cancellationToken)
        {
            var existNutritionPlan = _unitOfWork.NutritionPlanRepository.Get(filter: np => np.NutritionPlanId.Equals(request.NutritionPlanId) && np.IsDeleted == false).FirstOrDefault();
            if (existNutritionPlan == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Chế độ dinh dưỡng không tồn tại");
            }

            var existFeedSession = _unitOfWork.FeedSessionRepository.Get(filter: f => f.FeedSessionId.Equals(request.FeedSessionId) && f.IsDeleted == false).FirstOrDefault();
            if (existFeedSession == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Phiên cho ăn không tồn tại");
            }

            try
            {
                existNutritionPlan.FeedSessions.Remove(existFeedSession);

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
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra");
            }
        }
    }
}
