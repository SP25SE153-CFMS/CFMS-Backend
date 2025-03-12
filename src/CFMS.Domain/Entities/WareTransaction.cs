using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public class WareTransaction : EntityAudit
{
    public Guid TransactionId { get; set; }

    public Guid? WareId { get; set; }

    public Guid? ProductId { get; set; }

    public int? Quantity { get; set; }

    public string? TransactionType { get; set; }

    public string? Reason { get; set; }

    public DateTime? TransactionDate { get; set; }

    public Guid? LocationFrom { get; set; }

    public Guid? LocationTo { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Warehouse? Ware { get; set; }
}
