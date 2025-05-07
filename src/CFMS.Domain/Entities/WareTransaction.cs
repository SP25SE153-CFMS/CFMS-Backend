using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class WareTransaction : EntityAudit
{
    public Guid TransactionId { get; set; }

    public Guid? WareId { get; set; }

    public Guid? ResourceId { get; set; }

    public int? Quantity { get; set; }

    public Guid? UnitId { get; set; }

    public int? BatchNumber { get; set; }

    public Guid? TransactionType { get; set; }

    public string? Reason { get; set; }

    public DateTime? TransactionDate { get; set; }

    public Guid? LocationFromId { get; set; }

    public Guid? LocationToId { get; set; }

    public virtual Warehouse? LocationFrom { get; set; }

    public virtual Warehouse? LocationTo { get; set; }

    public virtual SubCategory? TransactionTypeNavigation { get; set; }

    public virtual Warehouse? Ware { get; set; }

    public decimal CurrentQuantity { get; set; }
}
