using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.DTOs.Receipt
{
    public class InventoryReceiptDetailDto
    {
        //public Guid? InventoryReceiptId { get; set; }

        public Guid? ResourceId { get; set; }

        public decimal? ActualQuantity { get; set; }

        public Guid? UnitId { get; set; }

        public string? Note { get; set; }

        public int? BatchNumber { get; set; }
    }
}
