using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class InventoryRequestDetail : EntityAudit
{
    public Guid InventoryRequestDetailId { get; set; }

    public Guid? InventoryRequestId { get; set; }

    public Guid? ResourceId { get; set; }

    public decimal? ExpectedQuantity { get; set; }

    public Guid? UnitId { get; set; }

    public string? Reason { get; set; }

    public DateTime? ExpectedDate { get; set; }

    public string? Note { get; set; }

    public virtual InventoryRequest? InventoryRequest { get; set; }

    public virtual Resource? Resource { get; set; }

    public virtual SubCategory? Unit { get; set; }
}
