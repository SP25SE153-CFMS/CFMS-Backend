using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.GetBatch
{
    public class GetBatchQuery : IRequest<BaseResponse<ChickenBatch>>
    {
        public GetBatchQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
