using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class Warehouse : EntityAudit
{
    public Guid WareId { get; set; }

    public Guid? FarmId { get; set; }

    public Guid? ResourceTypeId { get; set; }

    public string? WarehouseName { get; set; }

    public int? MaxQuantity { get; set; }

    public decimal? MaxWeight { get; set; }

    public int? CurrentQuantity { get; set; }

    public decimal? CurrentWeight { get; set; }

    public string? Description { get; set; }

    public int? Status { get; set; }

    public virtual Farm? Farm { get; set; }

    public virtual ICollection<InventoryRequest> InventoryRequestWareFroms { get; set; } = new List<InventoryRequest>();

    public virtual ICollection<InventoryRequest> InventoryRequestWareTos { get; set; } = new List<InventoryRequest>();

    public virtual SubCategory? ResourceType { get; set; }

    public virtual ICollection<TaskLocation> TaskLocations { get; set; } = new List<TaskLocation>();

    public virtual SystemConfig Ware { get; set; } = null!;

    public virtual ICollection<WarePermission> WarePermissions { get; set; } = new List<WarePermission>();

    public virtual ICollection<WareStock> WareStocks { get; set; } = new List<WareStock>();

    public virtual ICollection<WareTransaction> WareTransactionLocationFroms { get; set; } = new List<WareTransaction>();

    public virtual ICollection<WareTransaction> WareTransactionLocationTos { get; set; } = new List<WareTransaction>();

    public virtual ICollection<WareTransaction> WareTransactionWares { get; set; } = new List<WareTransaction>();
}
