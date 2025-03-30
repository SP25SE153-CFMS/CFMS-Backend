using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.AssignmentFeat.Delete
{
    public class DeleteAssignmentCommand : IRequest<BaseResponse<bool>>
    {
        public DeleteAssignmentCommand(Guid assignmentId)
        {
            AssignmentId = assignmentId;
        }

        public Guid AssignmentId { get; set; }
    }
}
