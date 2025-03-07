using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class Product
{
    public Guid ProductId { get; set; }

    public Guid? ProductTypeId { get; set; }

    public int? Quantity { get; set; }

    public string? Unit { get; set; }

    public string? Package { get; set; }

    public string? Usage { get; set; }

    public virtual Food Product1 { get; set; } = null!;

    public virtual HarvestProduct Product2 { get; set; } = null!;

    public virtual Vaccine Product3 { get; set; } = null!;

    public virtual Equipment ProductNavigation { get; set; } = null!;

    public virtual SubCategory? ProductType { get; set; }

    public virtual ICollection<WareTransaction> WareTransactions { get; set; } = new List<WareTransaction>();

    public virtual ICollection<WarehouseStock> WarehouseStocks { get; set; } = new List<WarehouseStock>();
}
