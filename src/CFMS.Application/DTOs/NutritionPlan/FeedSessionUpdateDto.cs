namespace CFMS.Application.DTOs.NutritionPlan
{
    public class FeedSessionUpdateDto
    {
        public Guid? FeedSessionId { get; set; }

        public TimeOnly? StartTime { get; set; }

        public TimeOnly? EndTime { get; set; }

        public decimal? FeedAmount { get; set; }

        public Guid? UnitId { get; set; }

        public string? Note { get; set; }
    }
}
