using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CFMS.Application.DTOs.WareStock
{
    public class WareStockHavestProductResponse : WareStockResponseBase
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Guid? ResourceId { get; set; }

        public Guid HarvestProductId { get; set; }

        public string? HarvestProductName { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Guid? HarvestProductTypeId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? HarvestProductTypeName { get; set; }
    }
}
