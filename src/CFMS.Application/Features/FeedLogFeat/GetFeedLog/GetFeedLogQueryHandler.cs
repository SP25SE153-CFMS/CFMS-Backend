using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.FeedLogFeat.GetFeedLog
{
    public class GetFeedLogQueryHandler : IRequestHandler<GetFeedLogQuery, BaseResponse<FeedLog>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetFeedLogQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<FeedLog>> Handle(GetFeedLogQuery request, CancellationToken cancellationToken)
        {
            var existFeedLog = _unitOfWork.FeedLogRepository.Get(filter: f => f.FeedLogId.Equals(request.FeedLogId) && f.IsDeleted == false).FirstOrDefault();
            if (existFeedLog == null)
            {
                return BaseResponse<FeedLog>.FailureResponse(message: "FeedLog không tồn tại");
            }
            return BaseResponse<FeedLog>.SuccessResponse(data: existFeedLog);
        }
    }
}
