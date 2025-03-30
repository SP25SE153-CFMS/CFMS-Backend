using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.AssignmentFeat.Update
{
    public class UpdateAssignmentCommand : IRequest<BaseResponse<bool>>
    {
    }
}
