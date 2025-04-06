using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.TaskFeat.CompleteTask
{
    public class CompleteTaskCommand : IRequest<BaseResponse<bool>>
    {
        public CompleteTaskCommand(Guid taskId, int status)
        {
            TaskId = taskId;
            Status = status;
        }

        public Guid TaskId { get; set; }

        public int Status { get; set; }
    }
}
