using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.DTOs.Supplier
{
    public class ResourceSupplierChickenResponse : ResourceSupplierResponseBase
    {
        public Guid ChickenId { get; set; }

        public string? ChickenCode { get; set; }

        public string? ChickenName { get; set; }

        public string? DescriptionOfChicken { get; set; }

        //public int? Status { get; set; }

        public string? ChickenType { get; set; }
    }
}
