using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.FarmFeat.UpdateFarmEmployee
{
    public class UpdateFarmEmployeeCommand : IRequest<BaseResponse<bool>>
    {
        public UpdateFarmEmployeeCommand(Guid? farmId, DateTime? startDate, DateTime? endDate, int? status, int? farmRole, Guid? farmEmployeeId)
        {
            FarmId = farmId;
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
            FarmRole = farmRole;
            FarmEmployeeId = farmEmployeeId;
        }

        public Guid? FarmId { get; set; }

        public Guid? FarmEmployeeId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? Status { get; set; }

        public int? FarmRole { get; set; }
    }
}
