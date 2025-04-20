using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.AssignmentFeat.GetAssignmentByFarmId
{
    public class GetAssignmentByFarmIdQuery : IRequest<BaseResponse<IEnumerable<Assignment>>>
    {
        public GetAssignmentByFarmIdQuery(Guid farmId)
        {
            FarmId = farmId;
        }

        public Guid FarmId { get; set; }
    }
}
