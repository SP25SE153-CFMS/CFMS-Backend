using System;
using System.Collections.Generic;

namespace CFMS.Infrastructure.Persistence;

public partial class HarvestProduct
{
    public Guid HarvestProductId { get; set; }

    public string? HarvestProductName { get; set; }

    public int? Quantity { get; set; }

    public string? Note { get; set; }

    public Guid? UnitId { get; set; }

    public Guid? ProductId { get; set; }

    public virtual ICollection<HarvestTask> HarvestTasks { get; set; } = new List<HarvestTask>();

    public virtual Product? Product { get; set; }

    public virtual SubCategory? Unit { get; set; }
}
