using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.FeedLogFeat.GetFeedLogs
{
    public class GetFeedLogsQueryHandler : IRequestHandler<GetFeedLogsQuery, BaseResponse<IEnumerable<FeedLog>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetFeedLogsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<IEnumerable<FeedLog>>> Handle(GetFeedLogsQuery request, CancellationToken cancellationToken)
        {
            var feedLogs = _unitOfWork.FeedLogRepository.Get(filter: f => f.IsDeleted == false && f.ChickenBatchId.Equals(request.ChickBatchId));
            return BaseResponse<IEnumerable<FeedLog>>.SuccessResponse(data: feedLogs);
        }
    }
}
