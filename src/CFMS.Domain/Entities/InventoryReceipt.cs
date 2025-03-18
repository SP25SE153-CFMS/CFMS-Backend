using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public class InventoryReceipt : EntityAudit
{
    public Guid InventoryReceiptId { get; set; }

    public Guid? InventoryRequestId { get; set; }

    public Guid? ReceiptTypeId { get; set; }

    public Guid? WareFromId { get; set; }

    public Guid? WareToId { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<InventoryReceiptDetail> InventoryReceiptDetails { get; set; } = new List<InventoryReceiptDetail>();

    public virtual InventoryRequest? InventoryRequest { get; set; }

    public virtual SubCategory? ReceiptType { get; set; }
}
