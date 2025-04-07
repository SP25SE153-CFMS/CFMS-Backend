using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.DTOs.Warehouse
{
    public class WareResponse
    {
        public Guid? WareId { get; set; }

        public Guid? FarmId { get; set; }

        public Guid? ResourceTypeId { get; set; }

        public string? ResourceTypeName { get; set; }

        public string? WarehouseName { get; set; }

        public int? MaxQuantity { get; set; }

        public decimal? MaxWeight { get; set; }

        public int? CurrentQuantity { get; set; }

        public decimal? CurrentWeight { get; set; }

        public string? Description { get; set; }

        public int? Status { get; set; }
    }
}
