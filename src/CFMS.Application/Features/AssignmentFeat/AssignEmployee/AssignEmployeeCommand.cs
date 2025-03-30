using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.AssignmentFeat.AssignEmployee
{
    public class AssignEmployeeCommand : IRequest<BaseResponse<bool>>
    {
    }
}
