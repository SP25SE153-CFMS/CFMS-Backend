using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.DTOs.Supplier
{
    public class SupplierResponse
    {
        public Guid? SupplierId { get; set; }
        public string? SupplierCode { get; set; }
        public string? SupplierName { get; set; }
        public Guid? ResourceSupplierId { get; set; }
    }
}
