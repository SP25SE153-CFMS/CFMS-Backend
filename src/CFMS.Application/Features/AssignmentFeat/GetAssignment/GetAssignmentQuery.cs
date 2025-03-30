using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.AssignmentFeat.GetAssignment
{
    public class GetAssignmentQuery : IRequest<BaseResponse<Assignment>>
    {
        public GetAssignmentQuery(Guid assignmentId)
        {
            AssignmentId = assignmentId;
        }

        public Guid AssignmentId { get; set; }
    }
}
