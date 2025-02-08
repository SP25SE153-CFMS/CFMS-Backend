using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class Breadingarea
{
    public Guid Breadingareaid { get; set; }

    public string Breadingareacode { get; set; } = null!;

    public string Breadingareaname { get; set; } = null!;

    public int? Mealsperday { get; set; }

    public decimal? Humidity { get; set; }

    public decimal? Temperature { get; set; }

    public decimal? Weight { get; set; }

    public string? Image { get; set; }

    public string? Notes { get; set; }

    public decimal? Area { get; set; }

    public bool? Covered { get; set; }

    public Guid Farmid { get; set; }

    public string? Breadingpurpose { get; set; }

    public virtual ICollection<Breadingequipment> Breadingequipments { get; set; } = new List<Breadingequipment>();

    public virtual ICollection<Chickenbatch> Chickenbatches { get; set; } = new List<Chickenbatch>();

    public virtual Farm Farm { get; set; } = null!;
}
