using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.TaskFeat.GetTasks
{
    public class GetTasksQuery : IRequest<BaseResponse<IEnumerable<Domain.Entities.Task>>>
    {
        public GetTasksQuery() { }
    }
}
