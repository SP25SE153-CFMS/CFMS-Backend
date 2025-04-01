using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.FarmFeat.DeleteFarmEmployee
{
    public class DeleteFarmEmployeeCommand : IRequest<BaseResponse<bool>>
    {
        public DeleteFarmEmployeeCommand(Guid? farmEmployeeId)
        {
            FarmEmployeeId = farmEmployeeId;
        }

        public Guid? FarmEmployeeId { get; set; }
    }
}
