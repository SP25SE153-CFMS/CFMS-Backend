using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public class Resource : EntityAudit
{
    public Guid ResourceId { get; set; }

    public Guid? ResourceTypeId { get; set; }

    public string? Description { get; set; }

    public Guid? UnitId { get; set; }

    public Guid? PackageId { get; set; }

    public decimal? PackageSize { get; set; }

    public virtual Equipment? Equipment { get; set; }

    public virtual Food? Food { get; set; }

    public virtual ICollection<InventoryRequestDetail> InventoryRequestDetails { get; set; } = new List<InventoryRequestDetail>();

    public virtual Medicine? Medicine { get; set; }

    public virtual SubCategory? Package { get; set; }

    public virtual ICollection<ResourceSupplier> ResourceSuppliers { get; set; } = new List<ResourceSupplier>();

    public virtual SubCategory? ResourceType { get; set; }

    public virtual SubCategory? Unit { get; set; }

    public virtual ICollection<WareStock> WareStocks { get; set; } = new List<WareStock>();
}
