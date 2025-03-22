using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class InventoryReceiptDetail : EntityAudit
{
    public Guid InventoryReceiptDetailId { get; set; }

    public Guid? InventoryReceiptId { get; set; }

    public decimal? ActualQuantity { get; set; }

    public DateTime? ActualDate { get; set; }

    public string? Note { get; set; }

    public int? BatchNumber { get; set; }

    public virtual InventoryReceipt? InventoryReceipt { get; set; }
}
