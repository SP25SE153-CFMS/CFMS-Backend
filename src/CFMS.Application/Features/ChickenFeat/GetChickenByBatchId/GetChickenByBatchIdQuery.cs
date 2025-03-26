using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.ChickenFeat.GetChickenByBatchId
{
    public class GetChickenByBatchIdQuery : IRequest<BaseResponse<Chicken>>
    {
        public GetChickenByBatchIdQuery(Guid chickenBatchId)
        {
            ChickenBatchId = chickenBatchId;
        }

        public Guid ChickenBatchId { get; set; }
    }
}
