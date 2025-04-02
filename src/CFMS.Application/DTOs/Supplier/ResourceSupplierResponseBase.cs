using CFMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.DTOs.Supplier
{
    public abstract class ResourceSupplierResponseBase
    {
        public string ResourceType { get; set; }

        public string UnitSpecification { get; set; }

        public string? Description { get; set; }

        public decimal? Price { get; set; } 
    }
}
