using CFMS.Application.Common;
using CFMS.Application.DTOs.ChickenBatch;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.GetBatch
{
    public class GetBatchQuery : IRequest<BaseResponse<ChickenBatchResponse>>
    {
        public GetBatchQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
