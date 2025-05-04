using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.StockReceipt.GetStockReceipts
{
    public class GetStockReceiptsQuery : IRequest<BaseResponse<IEnumerable<Domain.Entities.StockReceipt>>>
    {
        public GetStockReceiptsQuery(Guid farmId)
        {
            FarmId = farmId;
        }

        public Guid FarmId { get; set; }
    }
}
