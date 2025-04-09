using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.DTOs.WareStock
{
    public class WareStockEquipmentResponse : WareStockResponseBase
    {
        public Guid ResourceId { get; set; }

        public Guid EquipmentId { get; set; }

        public string? EquipmentCode { get; set; }

        public string? EquipmentName { get; set; }

        public Guid? MaterialId { get; set; }

        public string? Material { get; set; }

        public string? Usage { get; set; }

        public int? Warranty { get; set; }

        public Guid? SizeUnitId { get; set; }

        public decimal? Size { get; set; }

        public Guid? WeightUnitId { get; set; }

        public decimal? Weight { get; set; }

        public DateTime? PurchaseDate { get; set; }
    }
}
