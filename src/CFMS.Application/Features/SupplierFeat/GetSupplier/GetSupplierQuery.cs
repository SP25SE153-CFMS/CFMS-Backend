using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.SupplierFeat.GetSupplier
{
    public class GetSupplierQuery : IRequest<BaseResponse<Supplier>>
    {
        public GetSupplierQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
