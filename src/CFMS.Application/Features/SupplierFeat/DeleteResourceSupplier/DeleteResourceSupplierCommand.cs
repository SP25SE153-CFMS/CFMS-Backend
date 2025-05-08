using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.SupplierFeat.DeleteResourceSupplier
{
    public class DeleteResourceSupplierCommand : IRequest<BaseResponse<bool>>
    {
        public DeleteResourceSupplierCommand(Guid id)
        {
            ResourceSupplierId = id;
        }

        public Guid ResourceSupplierId { get; set; }
    }
}
