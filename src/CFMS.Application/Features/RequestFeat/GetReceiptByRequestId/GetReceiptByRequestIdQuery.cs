using CFMS.Application.Common;
using CFMS.Application.DTOs.Request;
using CFMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.RequestFeat.GetReceiptByRequestId
{
    public class GetReceiptByRequestIdQuery : IRequest<BaseResponse<ReceiptDto>>
    {
        public GetReceiptByRequestIdQuery(Guid inventoryRequestId)
        {
            InventoryRequestId = inventoryRequestId;
        }

        public Guid InventoryRequestId { get; set; }
    }
}
