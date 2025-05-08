using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.NutritionPlanFeat.AddFeedSession
{
    public class AddFeedSessionCommandHandler : IRequestHandler<AddFeedSessionCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddFeedSessionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(AddFeedSessionCommand request, CancellationToken cancellationToken)
        {
            var existNutritionPlan = _unitOfWork.NutritionPlanRepository.Get(filter: np => np.NutritionPlanId.Equals(request.NutritionPlanId) && np.IsDeleted == false).FirstOrDefault();
            if (existNutritionPlan == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Chế độ dinh dưỡng không tồn tại");
            }

            try
            {
                existNutritionPlan.FeedSessions.Add(new FeedSession
                {
                    NutritionPlanId = request.NutritionPlanId,
                    StartTime = request.StartTime,
                    EndTime = request.EndTime,
                    FeedAmount = request.FeedAmount,
                    UnitId = request.UnitId,
                    Note = request.Note,
                });

                _unitOfWork.NutritionPlanRepository.Update(existNutritionPlan);
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
