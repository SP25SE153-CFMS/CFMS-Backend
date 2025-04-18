using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CFMS.Domain.Entities;

public partial class InventoryRequestDetail
{
    public Guid InventoryRequestDetailId { get; set; }

    public Guid? InventoryRequestId { get; set; }

    public Guid? ResourceId { get; set; }

    public Guid? ResourceSupplierId { get; set; }

    public decimal? ExpectedQuantity { get; set; }

    public Guid? UnitId { get; set; }

    public string? Reason { get; set; }

    public DateTime? ExpectedDate { get; set; }

    public string? Note { get; set; }

    [JsonIgnore]
    public virtual InventoryRequest? InventoryRequest { get; set; }

    public virtual Resource? Resource { get; set; }

    public virtual ResourceSupplier? ResourceSupplier { get; set; }

    public virtual SubCategory? Unit { get; set; }
}
