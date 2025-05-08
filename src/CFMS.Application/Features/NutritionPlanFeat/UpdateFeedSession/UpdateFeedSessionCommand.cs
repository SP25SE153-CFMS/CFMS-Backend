using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.NutritionPlanFeat.UpdateFeedSession
{
    public class UpdateFeedSessionCommand : IRequest<BaseResponse<bool>>
    {
        public UpdateFeedSessionCommand(Guid feedSessionId, TimeOnly? startTime, TimeOnly? endTime, decimal? feedAmount, Guid? unitId, string? note)
        {
            FeedSessionId = feedSessionId;
            StartTime = startTime;
            EndTime = endTime;
            FeedAmount = feedAmount;
            UnitId = unitId;
            Note = note;
        }

        public Guid FeedSessionId { get; set; }

        public TimeOnly? StartTime { get; set; }

        public TimeOnly? EndTime { get; set; }

        public decimal? FeedAmount { get; set; }

        public Guid? UnitId { get; set; }

        public string? Note { get; set; }
    }
}
