using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.AssignmentFeat.Update
{
    public class UpdateAssignmentCommand : IRequest<BaseResponse<bool>>
    {
        public UpdateAssignmentCommand(Guid taskId, DateTime? assignedDate, int? status, string? note, Guid assignmentId)
        {
            TaskId = taskId;
            AssignedDate = assignedDate;
            Status = status;
            Note = note;
            AssignmentId = assignmentId;
        }

        public Guid TaskId { get; set; }

        public Guid AssignmentId { get; set; }

        public DateTime? AssignedDate { get; set; }

        public int? Status { get; set; }

        public string? Note { get; set; }
    }
}
