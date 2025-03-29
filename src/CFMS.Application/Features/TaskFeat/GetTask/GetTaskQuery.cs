using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.TaskFeat.GetTask
{
    public class GetTaskQuery : IRequest<BaseResponse<Domain.Entities.Task>>
    {
        public GetTaskQuery(Guid taskId)
        {
            TaskId = taskId;
        }

        public Guid TaskId { get; set; }
    }
}
