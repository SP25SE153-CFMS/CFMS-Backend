using CFMS.Application.Common;
using CFMS.Application.DTOs.Receipt;
using MediatR;
using System;
using System.Collections.Generic;

namespace CFMS.Application.Features.InventoryReceipts.Commands
{
    public class CreateInventoryReceiptCommand : IRequest<BaseResponse<bool>>
    {
        public CreateInventoryReceiptCommand(Guid requestId, Guid inventoryRequestId, Guid receiptTypeId, List<InventoryReceiptDetailDto> receiptDetails, Guid? wareToId, Guid? wareFromId)
        {
            RequestId = requestId;
            InventoryRequestId = inventoryRequestId;
            ReceiptTypeId = receiptTypeId;
            ReceiptDetails = receiptDetails;
            WareToId = wareToId;
            WareFromId = wareFromId;
        }

        public Guid RequestId { get; set; }
        public Guid InventoryRequestId { get; set; }
        public Guid ReceiptTypeId { get; set; }
        public Guid? WareFromId { get; set; }
        public Guid? WareToId { get; set; }
        public List<InventoryReceiptDetailDto> ReceiptDetails { get; set; }
    }
}
