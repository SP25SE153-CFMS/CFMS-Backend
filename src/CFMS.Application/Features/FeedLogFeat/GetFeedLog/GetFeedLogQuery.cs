using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.FeedLogFeat.GetFeedLog
{
    public class GetFeedLogQuery : IRequest<BaseResponse<FeedLog>>
    {
        public GetFeedLogQuery(Guid feedLogId)
        {
            FeedLogId = feedLogId;
        }

        public Guid FeedLogId { get; set; }
    }
}
