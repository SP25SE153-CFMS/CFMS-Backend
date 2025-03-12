using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public class StockReceipt : EntityAudit
{
    public Guid StockRepId { get; set; }

    public Guid? DetailId { get; set; }

    public string? StockReceiptType { get; set; }

    public string? ItemType { get; set; }

    public int? ActualQuantity { get; set; }

    public string? LocationFrom { get; set; }

    public string? LocationTo { get; set; }

    public virtual RequestDetail? Detail { get; set; }
}
