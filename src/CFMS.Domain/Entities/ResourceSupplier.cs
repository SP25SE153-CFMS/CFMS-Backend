using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public class ResourceSupplier : EntityAudit
{
    public Guid ResourceSupplierId { get; set; }

    public Guid? ResourceId { get; set; }

    public string? Description { get; set; }

    public Guid? SupplierId { get; set; }

    public decimal? Price { get; set; }

    public Guid? UnitPriceId { get; set; }

    public Guid? PackagePriceId { get; set; }

    public decimal? PackageSizePrice { get; set; }

    public virtual SubCategory? PackagePrice { get; set; }

    public virtual Resource? Resource { get; set; }

    public virtual SubCategory? Supplier { get; set; }

    public virtual SubCategory? UnitPrice { get; set; }
}
