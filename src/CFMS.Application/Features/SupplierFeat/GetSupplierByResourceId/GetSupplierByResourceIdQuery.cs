using CFMS.Application.Common;
using CFMS.Application.DTOs.Supplier;
using CFMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.SupplierFeat.GetSupplierByResourceId
{
    public class GetSupplierByResourceIdQuery : IRequest<BaseResponse<IEnumerable<SupplierResponse>>>
    {
        public Guid ResourceId { get; set; }

        public GetSupplierByResourceIdQuery(Guid resourceId)
        {
            ResourceId = resourceId;
        }
    }
}
