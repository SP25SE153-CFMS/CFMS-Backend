using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.StockReceipt.GetStockReceipt
{
    public class GetStockReceiptQuery : IRequest<BaseResponse<Domain.Entities.StockReceipt>>
    {
        public GetStockReceiptQuery(Guid stockReceiptId)
        {
            StockReceiptId = stockReceiptId;
        }

        public Guid StockReceiptId { get; set; }
    }
}
