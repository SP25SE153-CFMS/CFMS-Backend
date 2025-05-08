using CFMS.Application.Common;
using CFMS.Application.DTOs.Receipt;
using CFMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.RequestFeat.GetReceiptByFarmId
{
    public class GetReceiptByFarmIdQuery : IRequest<BaseResponse<IEnumerable<ReceiptResponse>>>
    {
        public GetReceiptByFarmIdQuery(Guid farmId)
        {
            FarmId = farmId;
        }

        public Guid FarmId { get; set; }
    }
}
