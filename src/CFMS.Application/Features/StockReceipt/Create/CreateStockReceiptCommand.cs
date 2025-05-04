using CFMS.Application.Common;
using CFMS.Application.DTOs.StockReceipt;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.StockReceipt.Create
{
    public class CreateStockReceiptCommand : IRequest<BaseResponse<bool>>
    {
        public CreateStockReceiptCommand(Guid? receiptTypeId, Guid? farmId, IEnumerable<StockReceiptDetailRequest> stockReceiptDetails)
        {
            ReceiptTypeId = receiptTypeId;
            FarmId = farmId;
            StockReceiptDetails = stockReceiptDetails;
        }

        public Guid? ReceiptTypeId { get; set; }

        public Guid? FarmId { get; set; }

        public IEnumerable<StockReceiptDetailRequest> StockReceiptDetails { get; set; }
    }
}
