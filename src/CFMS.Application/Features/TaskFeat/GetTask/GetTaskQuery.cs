using CFMS.Application.Common;
using CFMS.Application.DTOs.Task;
using MediatR;

namespace CFMS.Application.Features.TaskFeat.GetTask
{
    public class GetTaskQuery : IRequest<BaseResponse<TaskResponse>>
    {
        public GetTaskQuery(Guid taskId)
        {
            TaskId = taskId;
        }

        public Guid TaskId { get; set; }
    }
}
