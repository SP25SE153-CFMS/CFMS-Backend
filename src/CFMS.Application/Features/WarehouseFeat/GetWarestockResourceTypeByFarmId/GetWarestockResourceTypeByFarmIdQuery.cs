using CFMS.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.WarehouseFeat.GetWarestockResourceTypeByFarmId
{
    public class GetWarestockResourceTypeByFarmIdQuery : IRequest<BaseResponse<IEnumerable<object>>>
    {
        public GetWarestockResourceTypeByFarmIdQuery(string resourceTypeName, Guid farmId)
        {
            ResourceTypeName = resourceTypeName;
            FarmId = farmId;
        }

        public string ResourceTypeName { get; }
        public Guid FarmId { get; }
    }
}
