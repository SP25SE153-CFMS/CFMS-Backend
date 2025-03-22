using CFMS.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.SupplierFeat.Create
{ 
    public class CreateSupplierCommand : IRequest<BaseResponse<bool>>
    {
        public string? SupplierName { get; set; }

        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public Guid? UnitPriceId { get; set; }

        public Guid? PackagePriceId { get; set; }

        public decimal? PackageSizePrice { get; set; }
    }
}
