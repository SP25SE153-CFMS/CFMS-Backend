using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.FeedLogFeat.Delete
{
    public class DeleteFeedLogCommandHandler : IRequestHandler<DeleteFeedLogCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteFeedLogCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteFeedLogCommand request, CancellationToken cancellationToken)
        {
            var existFeedLog = _unitOfWork.FeedLogRepository.Get(filter: f => f.FeedLogId.Equals(request.FeedLogId) && f.IsDeleted == false).FirstOrDefault();
            if (existFeedLog == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "FeedLog không tồn tại");
            }

            try
            {
                _unitOfWork.FeedLogRepository.Delete(existFeedLog);
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
