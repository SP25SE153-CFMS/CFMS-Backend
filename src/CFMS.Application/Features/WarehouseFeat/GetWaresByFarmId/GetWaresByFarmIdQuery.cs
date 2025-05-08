using CFMS.Application.Common;
using CFMS.Application.DTOs.Warehouse;
using CFMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.WarehouseFeat.GetWaresByFarmId
{
    public class GetWaresByFarmIdQuery :IRequest<BaseResponse<IEnumerable<WareResponse>>>
    {
        public GetWaresByFarmIdQuery(Guid farmId)
        {
            FarmId = farmId;
        }

        public Guid FarmId { get; set; }
    }
}
