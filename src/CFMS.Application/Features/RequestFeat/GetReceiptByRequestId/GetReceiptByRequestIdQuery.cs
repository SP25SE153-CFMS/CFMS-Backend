using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.RequestFeat.GetReceiptByRequestId
{
    public class GetReceiptByRequestIdQuery : IRequest<BaseResponse<IEnumerable<InventoryReceipt>>>
    {
        public GetReceiptByRequestIdQuery(Guid inventoryRequestId)
        {
            InventoryRequestId = inventoryRequestId;
        }

        public Guid InventoryRequestId { get; set; }
    }
}
