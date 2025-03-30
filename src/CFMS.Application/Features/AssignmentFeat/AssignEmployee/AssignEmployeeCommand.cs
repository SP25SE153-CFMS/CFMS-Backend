using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.AssignmentFeat.AssignEmployee
{
    public class AssignEmployeeCommand : IRequest<BaseResponse<bool>>
    {
        public AssignEmployeeCommand(Guid? taskId, Guid? assignedToId, DateTime? assignedDate, int? status, string? note)
        {
            TaskId = taskId;
            AssignedToId = assignedToId;
            AssignedDate = assignedDate;
            Status = status;
            Note = note;
        }

        public Guid? TaskId { get; set; }

        public Guid? AssignedToId { get; set; }

        public DateTime? AssignedDate { get; set; }

        public int? Status { get; set; }

        public string? Note { get; set; }
    }
}
