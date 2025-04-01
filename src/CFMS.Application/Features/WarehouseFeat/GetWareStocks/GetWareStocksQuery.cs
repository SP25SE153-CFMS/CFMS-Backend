using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.WarehouseFeat.GetWareStocks
{
    public class GetWareStocksQuery : IRequest<BaseResponse<IEnumerable<object>>>
    {
        public Guid ResourceTypeId { get; }
        public Guid WareId { get; }

        public GetWareStocksQuery(Guid wareId, Guid resourceTypeId)
        {
            ResourceTypeId = resourceTypeId;
            WareId = wareId;
        }
    }
}
