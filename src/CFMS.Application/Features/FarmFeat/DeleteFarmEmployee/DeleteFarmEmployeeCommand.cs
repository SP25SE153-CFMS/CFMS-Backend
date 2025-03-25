using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.FarmFeat.DeleteFarmEmployee
{
    public class DeleteFarmEmployeeCommand : IRequest<BaseResponse<bool>>
    {
        public Guid? FarmId { get; set; }

        public Guid? FarmEmployeeId { get; set; }
    }
}
