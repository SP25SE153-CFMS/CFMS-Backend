using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CFMS.Domain.Entities;

public partial class Resource : EntityAudit
{
    public Guid ResourceId { get; set; }

    public Guid? ResourceTypeId { get; set; }

    //public string? Description { get; set; }

    public Guid? UnitId { get; set; }

    public Guid? PackageId { get; set; }

    public decimal? PackageSize { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Guid? FoodId { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Guid? EquipmentId { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Guid? MedicineId { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public virtual Equipment? Equipment { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public virtual Food? Food { get; set; }

    public virtual ICollection<InventoryRequestDetail> InventoryRequestDetails { get; set; } = new List<InventoryRequestDetail>();

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public virtual Medicine? Medicine { get; set; }

    public virtual SubCategory? Package { get; set; }

    public virtual ICollection<ResourceSupplier> ResourceSuppliers { get; set; } = new List<ResourceSupplier>();

    public virtual SubCategory? ResourceType { get; set; }

    public virtual SubCategory? Unit { get; set; }

    public virtual ICollection<WareStock> WareStocks { get; set; } = new List<WareStock>();
}
