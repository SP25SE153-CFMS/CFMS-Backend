using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.TaskFeat.Update
{
    public class UpdateTaskCommand : IRequest<BaseResponse<bool>>
    {
    }
}
