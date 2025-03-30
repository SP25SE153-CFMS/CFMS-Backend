using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.AssignmentFeat.AssignEmployee
{
    public class AssignEmployeeCommand : IRequest<BaseResponse<bool>>
    {
        public AssignEmployeeCommand(Guid taskId, Guid[] assignedToIds, DateTime? assignedDate, int? status, string? note)
        {
            TaskId = taskId;
            AssignedToIds = assignedToIds;
            AssignedDate = assignedDate;
            Status = status;
            Note = note;
        }

        public Guid TaskId { get; set; }

        public Guid[] AssignedToIds { get; set; }

        public DateTime? AssignedDate { get; set; }

        public int? Status { get; set; }

        public string? Note { get; set; }
    }
}
