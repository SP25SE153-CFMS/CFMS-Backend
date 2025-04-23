using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.NutritionPlanFeat.UpdateFeedSession
{
    public class UpdateFeedSessionCommandHandler : IRequestHandler<UpdateFeedSessionCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateFeedSessionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(UpdateFeedSessionCommand request, CancellationToken cancellationToken)
        {
            var existFeedSession = _unitOfWork.FeedSessionRepository.Get(filter: f => f.FeedSessionId.Equals(request.FeedSessionId) && f.IsDeleted == false).FirstOrDefault();
            if (existFeedSession == null)
            {
                return BaseResponse<bool>.SuccessResponse(message: "Phiên cho ăn không tồn tại");
            }

            try
            {
                existFeedSession.StartTime = request.StartTime;
                existFeedSession.EndTime = request.EndTime;
                existFeedSession.FeedAmount = request.FeedAmount;
                existFeedSession.UnitId = request.UnitId;
                existFeedSession.Note = request.Note;

                _unitOfWork.FeedSessionRepository.Update(existFeedSession);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Cập nhật thành công");
                }
                return BaseResponse<bool>.SuccessResponse(message: "Cập nhật không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra");
            }
        }
    }
}
