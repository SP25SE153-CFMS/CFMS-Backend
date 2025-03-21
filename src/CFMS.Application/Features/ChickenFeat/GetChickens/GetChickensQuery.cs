using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.ChickenFeat.GetChickens
{
    public class GetChickensQuery : IRequest<BaseResponse<IEnumerable<Chicken>>>
    {
        public GetChickensQuery(Guid chickenBatchId)
        {
            ChickenBatchId = chickenBatchId;
        }

        public Guid ChickenBatchId { get; set; }
    }
}
