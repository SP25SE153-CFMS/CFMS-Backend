using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.SupplierFeat.GetSuppliersByFarmId
{
    public class GetSuppliersByFarmIdQuery : IRequest<BaseResponse<IEnumerable<Supplier>>>
    {
        public GetSuppliersByFarmIdQuery(Guid farmId)
        {
            FarmId = farmId;
        }

        public Guid FarmId { get; set; }
    }
}
