using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public class Warehouse : EntityAudit
{
    public Guid WareId { get; set; }

    public Guid? FarmId { get; set; }

    public Guid? StorageTypeId { get; set; }

    public string? WarehouseName { get; set; }

    public double? MaxCapacity { get; set; }

    public double? CurrentCapacity { get; set; }

    public string? Description { get; set; }

    public int? Status { get; set; }

    public virtual Farm? Farm { get; set; }

    public virtual ICollection<InventoryRequest> InventoryRequestWareFroms { get; set; } = new List<InventoryRequest>();

    public virtual ICollection<InventoryRequest> InventoryRequestWareTos { get; set; } = new List<InventoryRequest>();

    public virtual SubCategory? StorageType { get; set; }

    public virtual ICollection<WarePermission> WarePermissions { get; set; } = new List<WarePermission>();

    public virtual ICollection<WareStock> WareStocks { get; set; } = new List<WareStock>();

    public virtual ICollection<WareTransaction> WareTransactionLocationFroms { get; set; } = new List<WareTransaction>();

    public virtual ICollection<WareTransaction> WareTransactionLocationTos { get; set; } = new List<WareTransaction>();

    public virtual ICollection<WareTransaction> WareTransactionWares { get; set; } = new List<WareTransaction>();
}
