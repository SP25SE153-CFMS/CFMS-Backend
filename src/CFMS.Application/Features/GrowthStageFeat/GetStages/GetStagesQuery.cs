using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.GrowthStageFeat.GetStages
{
    public class GetStagesQuery : IRequest<BaseResponse<IEnumerable<GrowthStage>>>
    {
        public GetStagesQuery(Guid farmId)
        {
            FarmId = farmId;
        }

        public Guid FarmId { get; set; }
    }
}
