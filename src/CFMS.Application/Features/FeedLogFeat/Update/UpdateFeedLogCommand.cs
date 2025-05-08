using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.FeedLogFeat.Update
{
    public class UpdateFeedLogCommand : IRequest<BaseResponse<bool>>
    {
        public UpdateFeedLogCommand(Guid? feedLogId, DateTime? feedingDate, decimal? actualFeedAmount, string? note)
        {
            FeedLogId = feedLogId;
            FeedingDate = feedingDate;
            ActualFeedAmount = actualFeedAmount;
            Note = note;
        }

        public Guid? FeedLogId { get; set; }

        public DateTime? FeedingDate { get; set; }

        public decimal? ActualFeedAmount { get; set; }

        public string? Note { get; set; }
    }
}
