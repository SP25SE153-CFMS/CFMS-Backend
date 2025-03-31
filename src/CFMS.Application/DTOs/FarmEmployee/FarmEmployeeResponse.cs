using CFMS.Application.DTOs.Auth;
using CFMS.Domain.Entities;

namespace CFMS.Application.DTOs.FarmEmployee
{
    public class FarmEmployeeResponse
    {
        public Guid? FarmId { get; set; }

        public Guid? UserId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? Status { get; set; }

        public int? FarmRole { get; set; }

        public UserResponse? User { get; set; }
    }
}
