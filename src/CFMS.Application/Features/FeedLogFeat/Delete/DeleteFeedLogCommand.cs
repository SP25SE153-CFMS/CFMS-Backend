using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.FeedLogFeat.Delete
{
    public class DeleteFeedLogCommand : IRequest<BaseResponse<bool>>
    {
        public DeleteFeedLogCommand(Guid feedLogId)
        {
            FeedLogId = feedLogId;
        }

        public Guid FeedLogId { get; set; }
    }
}
