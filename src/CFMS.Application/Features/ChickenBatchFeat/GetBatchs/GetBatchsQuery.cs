using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.GetBatchs
{
    public class GetBatchsQuery : IRequest<BaseResponse<IEnumerable<ChickenBatch>>>
    {
    }
}
