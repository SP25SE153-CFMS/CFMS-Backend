using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.DTOs.Request
{
    public class InventoryRequestDetailDto
    {
        public Guid? ResourceId { get; set; }
        public decimal? ExpectedQuantity { get; set; }
        public Guid? UnitId { get; set; }
        public string? Reason { get; set; }
        public DateTime? ExpectedDate { get; set; }
        public string? Note { get; set; }
    }
}
