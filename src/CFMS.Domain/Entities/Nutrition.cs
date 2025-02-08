using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class Nutrition
{
    public Guid Nutritionid { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string Targetaudience { get; set; } = null!;

    public string Developmentstage { get; set; } = null!;

    public Guid Foodid { get; set; }

    public Guid Waterid { get; set; }

    public Guid Feedscheduleid { get; set; }

    public Guid Flockid { get; set; }

    public virtual Feedschedule Feedschedule { get; set; } = null!;

    public virtual Flock Flock { get; set; } = null!;

    public virtual Food Food { get; set; } = null!;

    public virtual Water Water { get; set; } = null!;
}
