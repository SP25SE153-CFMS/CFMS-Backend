using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public class WarehouseStock : EntityAudit
{
    public Guid WareStockId { get; set; }

    public Guid? WareId { get; set; }

    public Guid? ProductId { get; set; }

    public int? Quantity { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Warehouse? Ware { get; set; }
}
