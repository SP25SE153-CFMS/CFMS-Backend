using CFMS.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.WarehouseFeat.GetWareStockDepentSupplier
{
    public class GetWareStockDepentSupplierQuery : IRequest<BaseResponse<IEnumerable<object>>>
    {
        public GetWareStockDepentSupplierQuery(Guid wareId, Guid resourceTypeId)
        {
            WareId = wareId;
            ResourceTypeId = resourceTypeId;
        }

        public Guid WareId { get; }

        public Guid ResourceTypeId { get; }
    }
}
