using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CFMS.Application.DTOs.WareStock
{
    public class WareStockChickenBreedingResponse : WareStockResponseBase
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Guid? ResourceId { get; set; }

        public Guid ChickenId { get; set; }

        public string? ChickenCode { get; set; }

        public string? ChickenName { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Description { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Guid? ChickenTypeId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ChickenTypeName { get; set; }
    }
}
