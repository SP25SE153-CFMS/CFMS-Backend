using CFMS.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.SupplierFeat.UpdateResourceSupplier
{
    public class UpdateResourceSupplierCommand : IRequest<BaseResponse<bool>>
    {
        public UpdateResourceSupplierCommand(Guid resourceSupplierId, Guid? resourceId, string? description, decimal? price)
        {
            ResourceSupplierId = resourceSupplierId;
            ResourceId = resourceId;
            Description = description;
            Price = price;
        }

        public Guid ResourceSupplierId { get; set; }

        public Guid? ResourceId { get; set; }

        public string? Description { get; set; }

        //public Guid? SupplierId { get; set; }

        public decimal? Price { get; set; }
    }
}
