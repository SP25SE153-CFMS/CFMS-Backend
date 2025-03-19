using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.Create
{
    public class CreateChickenBatchCommand : IRequest<BaseResponse<bool>>
    {
    }
}
