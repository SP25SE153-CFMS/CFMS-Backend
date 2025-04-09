using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.DTOs.WareStock
{
    public abstract class WareStockResponseBase
    {
        public string? SpecQuantity { get; set; }

        public string? UnitSpecification { get; set; }

        public Guid? SupplierId { get; set; }

        public string? SupplierName { get; set; }
    }
}
