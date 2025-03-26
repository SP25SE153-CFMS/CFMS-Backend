using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.FeedLogFeat.GetFeedLogs
{
    public class GetFeedLogsQuery : IRequest<BaseResponse<IEnumerable<FeedLog>>>
    {
        public GetFeedLogsQuery(Guid chickBatchId)
        {
            ChickBatchId = chickBatchId;
        }

        public Guid ChickBatchId { get; set; }
    }
}
