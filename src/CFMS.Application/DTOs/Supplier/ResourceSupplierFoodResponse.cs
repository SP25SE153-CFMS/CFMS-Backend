using CFMS.Application.DTOs.WareStock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.DTOs.Supplier
{
    public class ResourceSupplierFoodResponse : ResourceSupplierResponseBase
    {
        public string? FoodCode { get; set; }

        public string? FoodName { get; set; }

        public string? Note { get; set; }

        public DateTime? ProductionDate { get; set; }

        public DateTime? ExpiryDate { get; set; }
    }
}
