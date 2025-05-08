using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.DTOs.HarvestProduct
{
    public class HarvestProductRequest
    {
        public Guid? ResourceId { get; set; }

        public decimal? Quantity { get; set; }

        //public Guid? UnitId { get; set; }
    }
}
