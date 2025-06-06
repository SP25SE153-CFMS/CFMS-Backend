﻿using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class WareStock
{
    public Guid WareStockId { get; set; }

    public Guid? WareId { get; set; }

    public Guid? ResourceId { get; set; }

    public Guid? SupplierId { get; set; }

    public int? Quantity { get; set; }

    public virtual Resource? Resource { get; set; }

    public virtual Supplier? Supplier { get; set; }

    public virtual Warehouse? Ware { get; set; }
}
