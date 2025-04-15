using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.DTOs.WareStock
{
    public class WareStockChickenBreedingResponse : WareStockResponseBase
    {
        public Guid ResourceId { get; set; }

        public Guid ChickenId { get; set; }

        public string? ChickenCode { get; set; }

        public string? ChickenName { get; set; }

        public string? Description { get; set; }

        public Guid? ChickenTypeId { get; set; }

        public string? ChickenTypeName { get; set; }
    }
}
