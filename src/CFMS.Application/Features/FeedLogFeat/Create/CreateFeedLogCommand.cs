using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.FeedLogFeat.Create
{
    public class CreateFeedLogCommand : IRequest<BaseResponse<bool>>
    {
        public CreateFeedLogCommand(Guid? chickenBatchId, DateTime? feedingDate, decimal? actualFeedAmount, Guid? unitId, Guid? taskId, string? note)
        {
            ChickenBatchId = chickenBatchId;
            FeedingDate = feedingDate;
            ActualFeedAmount = actualFeedAmount;
            UnitId = unitId;
            TaskId = taskId;
            Note = note;
        }

        public Guid? ChickenBatchId { get; set; }

        public DateTime? FeedingDate { get; set; }

        public decimal? ActualFeedAmount { get; set; }

        public Guid? UnitId { get; set; }

        public Guid? TaskId { get; set; }

        public string? Note { get; set; }
    }
}
