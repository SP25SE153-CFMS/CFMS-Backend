using CFMS.Application.Common;
using CFMS.Application.DTOs.Assignment;
using MediatR;

namespace CFMS.Application.Features.AssignmentFeat.AssignEmployee
{
    public class AssignEmployeeCommand : IRequest<BaseResponse<bool>>
    {
        public AssignEmployeeCommand(Guid taskId, IEnumerable<AssignmentRequest> assignedTos, DateTime? assignedDate, string? note)
        {
            TaskId = taskId;
            AssignedTos = assignedTos;
            AssignedDate = assignedDate;
            Note = note;
        }

        public Guid TaskId { get; set; }

        public IEnumerable<AssignmentRequest> AssignedTos { get; set; }

        public DateTime? AssignedDate { get; set; }

        public string? Note { get; set; }
    }
}
