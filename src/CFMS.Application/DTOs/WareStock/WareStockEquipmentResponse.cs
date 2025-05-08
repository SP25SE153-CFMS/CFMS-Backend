using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CFMS.Application.DTOs.WareStock
{
    public class WareStockEquipmentResponse : WareStockResponseBase
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Guid? ResourceId { get; set; }

        public Guid EquipmentId { get; set; }

        public string? EquipmentCode { get; set; }
        
        public string? EquipmentName { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Guid? MaterialId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Material { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Usage { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Warranty { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Guid? SizeUnitId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public decimal? Size { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Guid? WeightUnitId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public decimal? Weight { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? PurchaseDate { get; set; }
    }
}
