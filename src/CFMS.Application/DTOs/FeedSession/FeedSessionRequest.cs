namespace CFMS.Application.DTOs.FeedSession
{
    public class FeedSessionRequest
    {
        public DateTime? FeedingTime { get; set; }

        public decimal? FeedAmount { get; set; }

        public Guid? UnitId { get; set; }

        public string? Note { get; set; }
    }
}
