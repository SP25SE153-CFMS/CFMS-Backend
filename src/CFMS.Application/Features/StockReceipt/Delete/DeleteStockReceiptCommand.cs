using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.StockReceipt.Delete
{
    public class DeleteStockReceiptCommand : IRequest<BaseResponse<bool>>
    {
        public DeleteStockReceiptCommand(Guid stockReceiptId)
        {
            StockReceiptId = stockReceiptId;
        }

        public Guid StockReceiptId { get; set; }
    }
}
