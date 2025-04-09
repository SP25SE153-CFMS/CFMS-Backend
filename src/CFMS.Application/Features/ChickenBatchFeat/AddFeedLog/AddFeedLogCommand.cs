using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.AddFeedLog
{
    public class AddFeedLogCommand : IRequest<BaseResponse<bool>>
    {
    }
}
