using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.AssignmentFeat.GetAssignments
{
    public class GetAssignmentsQuery : IRequest<BaseResponse<IEnumerable<Assignment>>>
    {
    }
}
