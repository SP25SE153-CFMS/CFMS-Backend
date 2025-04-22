using CFMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.DTOs.Request
{
    public class ReceiptDto
    {
        public int? AmountOfBatch { get; set; }
        
        public List<ActualQuantityDto>? AmountOfActualQuantity { get; set; }

        public List<InventoryReceipt> InventoryReceipts { get; set; } = new List<InventoryReceipt>();
    }

    public class ActualQuantityDto
    {
        public Guid? ResourceId { get; set; }
        public decimal? TotalQuantity { get; set; }
    }
}
