using CFMS.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.SupplierFeat.AddResourceSupplier
{
    public class AddResourceSupplierCommand : IRequest<BaseResponse<bool>>
    {
        public AddResourceSupplierCommand(Guid? resourceId, string? description, Guid? supplierId, decimal? price)
        {
            ResourceId = resourceId;
            Description = description;
            SupplierId = supplierId;
            Price = price;
        }

        public Guid? ResourceId { get; set; }

        public string? Description { get; set; }

        public Guid? SupplierId { get; set; }

        public decimal? Price { get; set; }
    }
}
