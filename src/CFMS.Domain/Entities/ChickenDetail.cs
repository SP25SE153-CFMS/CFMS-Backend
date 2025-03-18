using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public class ChickenDetail : EntityAudit
{
    public Guid ChickenDetailId { get; set; }

    public Guid? ChickenId { get; set; }

    public decimal? Weight { get; set; }

    public int? Quantity { get; set; }

    public int? Gender { get; set; }

    public virtual Chicken? Chicken { get; set; }
}
