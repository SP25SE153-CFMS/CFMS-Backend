using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.QuantityLogDetail
{
    public class QuantityLogDetailQuery : IRequest<BaseResponse<QuantityLog>>
    {
        public QuantityLogDetailQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
