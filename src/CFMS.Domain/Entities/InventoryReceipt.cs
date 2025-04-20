using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CFMS.Domain.Entities;

public partial class InventoryReceipt : EntityAudit
{
    public Guid InventoryReceiptId { get; set; }

    public Guid? InventoryRequestId { get; set; }

    public Guid? ReceiptTypeId { get; set; }

    public string? ReceiptCodeNumber { get; set; }

    //public int? Status { get; set; }

    public virtual ICollection<InventoryReceiptDetail> InventoryReceiptDetails { get; set; } = new List<InventoryReceiptDetail>();

    [JsonIgnore]
    public virtual InventoryRequest? InventoryRequest { get; set; }

    public virtual SubCategory? ReceiptType { get; set; }
}
