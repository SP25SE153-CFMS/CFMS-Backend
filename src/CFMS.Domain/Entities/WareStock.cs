using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public class WareStock : EntityAudit
{
    public Guid WareStockId { get; set; }

    public Guid? WareId { get; set; }

    public Guid? ResourceId { get; set; }

    public int Quantity { get; set; }

    public Guid? UnitId { get; set; }

    public virtual Resource? Resource { get; set; }

    public virtual Warehouse? Ware { get; set; }
}
