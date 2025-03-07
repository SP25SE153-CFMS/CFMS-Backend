using System;
using System.Collections.Generic;

namespace CFMS.Infrastructure.Persistence;

public partial class RequestDetail
{
    public Guid DetailId { get; set; }

    public Guid? RequestId { get; set; }

    public int? ExpectedQuantity { get; set; }

    public int? Price { get; set; }

    public Guid? ItemId { get; set; }

    public string? LocationFrom { get; set; }

    public string? LocationTo { get; set; }

    public virtual Request? Request { get; set; }

    public virtual ICollection<StockReceipt> StockReceipts { get; set; } = new List<StockReceipt>();
}
