using CFMS.Application.Common;
using CFMS.Application.DTOs.Auth;
using CFMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.SupplierFeat.GetSuppliers
{
    public class GetSuppliersQuery : IRequest<BaseResponse<IEnumerable<Supplier>>>
    {
        public GetSuppliersQuery() { }
    }
}
