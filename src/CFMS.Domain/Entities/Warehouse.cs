using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public class Warehouse : EntityAudit
{
    public Guid WareId { get; set; }

    public Guid? FarmId { get; set; }

    public string? WarehouseName { get; set; }

    public double? MaxCapacity { get; set; }

    public double? TotalWeight { get; set; }

    public int? TotalQuantity { get; set; }

    public string? Description { get; set; }

    public virtual Farm? Farm { get; set; }

    public virtual ICollection<WareTransaction> WareTransactions { get; set; } = new List<WareTransaction>();

    public virtual ICollection<WarehousePermission> WarehousePermissions { get; set; } = new List<WarehousePermission>();

    public virtual ICollection<WarehouseStock> WarehouseStocks { get; set; } = new List<WarehouseStock>();
}
