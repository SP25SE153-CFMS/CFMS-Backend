using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CFMS.Domain.Entities;

public partial class ResourceSupplier : EntityAudit
{
    public Guid ResourceSupplierId { get; set; }

    public Guid? ResourceId { get; set; }

    public string? Description { get; set; }

    public Guid? SupplierId { get; set; }

    public decimal? Price { get; set; }

    public virtual Resource? Resource { get; set; }

    [JsonIgnore]
    public virtual Supplier? Supplier { get; set; }

    //public virtual SubCategory? UnitPrice { get; set; }

    public virtual ICollection<InventoryRequestDetail> InventoryRequestDetails { get; set; } = new List<InventoryRequestDetail>();
}
