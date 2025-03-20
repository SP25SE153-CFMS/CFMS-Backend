using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.GrowthStageFeat.GetStages
{
    public class GetStagesQuery : IRequest<BaseResponse<IEnumerable<GrowthStage>>>
    {
    }
}
