using CFMS.Application.Common;
using CFMS.Application.DTOs.Supplier;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.SupplierFeat.GetSupplierByResourceSupplierId
{
    public class GetSupplierByResourceSupplierIdQuery : IRequest<BaseResponse<SupplierResponse>>
    {
        public Guid ResourceSupplierId { get; set; }

        public GetSupplierByResourceSupplierIdQuery(Guid resourceSupplierId)
        {
            ResourceSupplierId = resourceSupplierId;
        }
    }
}
