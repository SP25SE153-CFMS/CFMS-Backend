using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.TaskFeat.CompleteTask
{
    public class CompleteTaskCommand : IRequest<BaseResponse<bool>>
    {
        public CompleteTaskCommand(Guid taskId, string note)
        {
            TaskId = taskId;
            Note = note;
        }

        public Guid TaskId { get; set; }

        public String Note { get; set; }
    }
}
