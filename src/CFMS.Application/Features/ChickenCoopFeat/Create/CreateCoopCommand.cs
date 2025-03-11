using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.ChickenCoopFeat.Create
{
    public class CreateCoopCommand : IRequest<BaseResponse<bool>>
    {
    }
}
