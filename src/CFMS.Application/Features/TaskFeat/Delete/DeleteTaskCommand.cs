using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.TaskFeat.Delete
{
    public class DeleteTaskCommand : IRequest<BaseResponse<bool>>
    {
        public DeleteTaskCommand(Guid taskId)
        {
            TaskId = taskId;
        }

        public Guid TaskId { get; set; }
    }
}
