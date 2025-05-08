namespace CFMS.Application.DTOs.FeedSession
{
    public class FeedSessionRequest
    {
        public TimeOnly? StartTime { get; set; }

        public TimeOnly? EndTime { get; set; }

        public decimal? FeedAmount { get; set; }

        public Guid? UnitId { get; set; }

        public string? Note { get; set; }
    }
}
