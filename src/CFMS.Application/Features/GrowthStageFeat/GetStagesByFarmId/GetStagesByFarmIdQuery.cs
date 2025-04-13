using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.GrowthStageFeat.GetStagesByFarmId
{
    public class GetStagesByFarmIdQuery : IRequest<BaseResponse<IEnumerable<GrowthStage>>>
    {
        public Guid FarmId { get; set; }
    }
}
