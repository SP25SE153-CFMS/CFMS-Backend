using CFMS.Application.Common;
using CFMS.Application.DTOs.Supplier;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.SupplierFeat.GetResourceSuppliers
{
    public class GetResourceSuppliersQuery :IRequest<BaseResponse<IEnumerable<object>>>
    {
        public Guid SupplierId { get; set; }

        public GetResourceSuppliersQuery(Guid supplierId)
        {
            SupplierId = supplierId;
        }
    }
}
