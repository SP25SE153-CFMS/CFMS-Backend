using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.DTOs.WareStock
{
    public class WareStockHavestProductResponse : WareStockResponseBase
    {
        public Guid ResourceId { get; set; }

        public Guid HarvestProductId { get; set; }

        public string? HarvestProductName { get; set; }

        public Guid HarvestProductTypeId { get; set; }

        public string? HarvestProductTypeName { get; set; }
    }
}
