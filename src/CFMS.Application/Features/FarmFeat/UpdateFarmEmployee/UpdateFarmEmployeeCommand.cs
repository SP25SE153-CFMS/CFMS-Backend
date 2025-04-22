using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.FarmFeat.UpdateFarmEmployee
{
    public class UpdateFarmEmployeeCommand : IRequest<BaseResponse<bool>>
    {
        public UpdateFarmEmployeeCommand(Guid? farmId, DateTime? startDate, DateTime? endDate, int? status, int? farmRole, Guid? farmEmployeeId, string? mail, string? phoneNumber)
        {
            FarmId = farmId;
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
            FarmRole = farmRole;
            FarmEmployeeId = farmEmployeeId;
            Mail = mail;
            PhoneNumber = phoneNumber;
        }

        public Guid? FarmId { get; set; }

        public Guid? FarmEmployeeId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? Status { get; set; }

        public int? FarmRole { get; set; }

        public string? Mail { get; set; }

        public string? PhoneNumber { get; set; }
    }
}
