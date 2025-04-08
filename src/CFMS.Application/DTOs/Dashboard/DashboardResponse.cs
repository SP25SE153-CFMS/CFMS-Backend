using CFMS.Application.DTOs.Farm;
using CFMS.Domain.Entities;

namespace CFMS.Application.DTOs.Dashboard
{
    public class DashboardResponse
    {
        public int TotalQuantity { get; set; }

        public FarmResponse Farm { get; set; }
    }
}
