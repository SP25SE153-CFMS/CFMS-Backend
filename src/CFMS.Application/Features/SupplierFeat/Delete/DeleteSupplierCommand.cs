using CFMS.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.SupplierFeat.Delete
{
    public class DeleteSupplierCommand : IRequest<BaseResponse<bool>>
    {
        public DeleteSupplierCommand(Guid id)
        {
            SupplierId = id;
        }

        public Guid SupplierId { get; set; }
    }
}
