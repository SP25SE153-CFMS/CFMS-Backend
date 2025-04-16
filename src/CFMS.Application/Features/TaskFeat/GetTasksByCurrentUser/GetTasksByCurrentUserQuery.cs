using CFMS.Application.Common;
using CFMS.Application.DTOs.Task;
using MediatR;

namespace CFMS.Application.Features.TaskFeat.GetTasksByCurrentUser
{
    public class GetTasksByCurrentUserQuery : IRequest<BaseResponse<IEnumerable<TaskResponse>>>
    {
        public GetTasksByCurrentUserQuery(Guid farmId)
        {
            FarmId = farmId;
        }

        public Guid FarmId { get; set; }
    }
}
