using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.DTOs.WareStock
{
    public class WareStockFoodResponse : WareStockResponseBase
    {
        public Guid ResourceId { get; set; }

        public Guid FoodId { get; set; }

        public string? FoodCode { get; set; }

        public string? FoodName { get; set; }

        public string? Note { get; set; }

        public DateTime? ProductionDate { get; set; }

        public DateTime? ExpiryDate { get; set; }
    }
}
