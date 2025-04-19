using CFMS.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.WarehouseFeat.GetWareStock
{
    public class GetWareStockQuery : IRequest<BaseResponse<object>>
    {
        public Guid ResourceId { get; }

        public GetWareStockQuery(Guid resourceId)
        {
            ResourceId = resourceId;
        }
    }
}
