using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CFMS.Domain.Entities;

public partial class InventoryReceiptDetail
{
    public Guid InventoryReceiptDetailId { get; set; }

    public Guid? InventoryReceiptId { get; set; }

    public Guid? ResourceId { get; set; }

    public Guid? ResourceSupplierId { get; set; }

    public decimal? ActualQuantity { get; set; }

    public DateTime? ActualDate { get; set; }

    public string? Note { get; set; }

    [JsonIgnore]
    public virtual InventoryReceipt? InventoryReceipt { get; set; }

    public virtual Resource? Resource { get; set; }
}
