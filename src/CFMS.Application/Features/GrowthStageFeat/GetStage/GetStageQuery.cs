using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.GrowthStageFeat.GetStage
{
    public class GetStageQuery : IRequest<BaseResponse<GrowthStage>>
    {
        public GetStageQuery(Guid stageId)
        {
            StageId = stageId;
        }

        public Guid StageId { get; set; }
    }
}
