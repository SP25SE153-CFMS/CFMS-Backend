using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public class HarvestDetail : EntityAudit
{
    public Guid HarvestDetailId { get; set; }

    public Guid? HarvestLogId { get; set; }

    public Guid? TypeProductId { get; set; }

    public int? Quantity { get; set; }

    public string? Note { get; set; }

    public virtual HarvestLog? HarvestLog { get; set; }

    public virtual SubCategory? TypeProduct { get; set; }
}
