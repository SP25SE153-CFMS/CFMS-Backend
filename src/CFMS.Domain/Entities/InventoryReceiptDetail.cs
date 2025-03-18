using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public class InventoryReceiptDetail : EntityAudit
{
    public Guid InventoryReceiptDetailId { get; set; }

    public Guid? InventoryReceiptId { get; set; }

    public Guid? ResourceId { get; set; }

    public decimal? ActualQuantity { get; set; }

    public Guid? UnitId { get; set; }

    public string? Reason { get; set; }

    public DateTime? ActualDate { get; set; }

    public string? Note { get; set; }

    public virtual InventoryReceipt? InventoryReceipt { get; set; }
}
