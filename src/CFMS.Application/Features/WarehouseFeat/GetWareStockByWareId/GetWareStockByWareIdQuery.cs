using CFMS.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.WarehouseFeat.GetWareStockByWareId
{
    public class GetWareStockByWareIdQuery : IRequest<BaseResponse<object>>
    {
        public Guid ResourceId { get; }

        public Guid WareId { get; }

        public GetWareStockByWareIdQuery(Guid resourceId, Guid wareId)
        {
            ResourceId = resourceId;
            WareId = wareId;
        }
    }
}
