using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.FarmFeat.AddFarmEmployee
{
    public class AddFarmEmployeeCommand : IRequest<BaseResponse<bool>>
    {
        public AddFarmEmployeeCommand(Guid? farmId, Guid? userId, int? status, int? farmRole)
        {
            FarmId = farmId;
            UserId = userId;
            Status = status;
            FarmRole = farmRole;
        }

        public Guid? FarmId { get; set; }

        public Guid? UserId { get; set; }

        public int? Status { get; set; }

        public int? FarmRole { get; set; }
    }
}
