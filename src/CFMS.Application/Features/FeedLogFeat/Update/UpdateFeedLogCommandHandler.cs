using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.FeedLogFeat.Update
{
    public class UpdateFeedLogCommandHandler : IRequestHandler<UpdateFeedLogCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateFeedLogCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(UpdateFeedLogCommand request, CancellationToken cancellationToken)
        {
            var existFeedLog = _unitOfWork.FeedLogRepository.Get(filter: f => f.FeedLogId.Equals(request.FeedLogId) && f.IsDeleted == false).FirstOrDefault();
            if (existFeedLog == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Lịch sử cho ăn không tồn tại");
            }

            try
            {
                existFeedLog.ActualFeedAmount = request.ActualFeedAmount;
                existFeedLog.Note = request.Note;
                existFeedLog.FeedingDate = request.FeedingDate;

                _unitOfWork.FeedLogRepository.Update(existFeedLog);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Cập nhật thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Cập nhật không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra");
            }
        }
    }
}
