namespace CFMS.Application.DTOs.Dashboard
{
    public class DashboardResponse
    {
        public int? TotalChicken { get; set; }

        public int? TotalEmployee { get; set; }

        public int? TotalChickenDeath { get; set; }

        public IEnumerable<Domain.Entities.ChickenBatch>? ChickenBatches { get; set; }
    }
}
