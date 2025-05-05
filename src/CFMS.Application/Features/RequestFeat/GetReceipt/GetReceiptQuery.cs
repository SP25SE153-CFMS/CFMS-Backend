using CFMS.Application.Common;
using CFMS.Application.DTOs.Receipt;
using CFMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.RequestFeat.GetReceipt
{
    public class GetReceiptQuery : IRequest<BaseResponse<ReceiptResponse>>
    {
        public GetReceiptQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
