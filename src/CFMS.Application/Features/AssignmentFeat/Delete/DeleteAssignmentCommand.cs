using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.AssignmentFeat.Delete
{
    public class DeleteAssignmentCommand : IRequest<BaseResponse<bool>>
    {
        public DeleteAssignmentCommand(Guid assignmentId, Guid taskId)
        {
            AssignmentId = assignmentId;
            TaskId = taskId;
        }

        public Guid TaskId { get; set; }

        public Guid AssignmentId { get; set; }
    }
}
