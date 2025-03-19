using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.Update
{
    public class UpdateChickenBatchCommand : IRequest<BaseResponse<bool>>
    {
    }
}
