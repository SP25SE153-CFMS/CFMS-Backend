using CFMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.DTOs.Receipt
{
    public class ReceiptResponse
    {
        public Guid InventoryReceiptId { get; set; }

        public Guid? InventoryRequestId { get; set; }

        public Guid? ReceiptTypeId { get; set; }

        public string? ReceiptCodeNumber { get; set; }

        public int? BatchNumber { get; set; }

        public Guid? FarmId { get; set; }

        public Guid? WareFromId { get; set; }

        public Guid? WareToId { get; set; }

        public ICollection<InventoryReceiptDetail> InventoryReceiptDetails { get; set; } = new List<InventoryReceiptDetail>();
    }
}
