using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.TaskFeat.Update
{
    public class UpdateTaskCommand : IRequest<BaseResponse<bool>>
    {
        public UpdateTaskCommand(int status, Guid taskId)
        {
            Status = status;
            TaskId = taskId;
        }

        public int Status { get; set; }
        public Guid TaskId { get; set; }
    }
}
