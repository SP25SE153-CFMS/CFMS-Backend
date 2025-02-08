using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class Breadingequipment
{
    public Guid Breadingequipmentid { get; set; }

    public Guid Breadingareaid { get; set; }

    public Guid Equipmentid { get; set; }

    public int? Quantity { get; set; }

    public DateOnly Assigneddate { get; set; }

    public DateOnly? Maintaindate { get; set; }

    public string? Status { get; set; }

    public string? Note { get; set; }

    public virtual Breadingarea Breadingarea { get; set; } = null!;

    public virtual Equipment Equipment { get; set; } = null!;
}
