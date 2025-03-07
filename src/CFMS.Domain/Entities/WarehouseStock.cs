using System;
using System.Collections.Generic;

namespace CFMS.Infrastructure.Persistence;

public partial class WarehouseStock
{
    public Guid WareStockId { get; set; }

    public Guid? WareId { get; set; }

    public Guid? ProductId { get; set; }

    public int? Quantity { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Warehouse? Ware { get; set; }
}
