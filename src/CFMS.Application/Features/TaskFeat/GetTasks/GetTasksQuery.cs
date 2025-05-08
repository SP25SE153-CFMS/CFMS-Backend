using CFMS.Application.Common;
using CFMS.Application.DTOs.Task;
using MediatR;

namespace CFMS.Application.Features.TaskFeat.GetTasks
{
    public class GetTasksQuery : IRequest<BaseResponse<IEnumerable<TaskResponse>>>
    {
        public GetTasksQuery() { }
    }
}
