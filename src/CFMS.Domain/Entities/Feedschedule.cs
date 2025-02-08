using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class Feedschedule
{
    public Guid Feedscheduleid { get; set; }

    public TimeOnly Feedtime { get; set; }

    public decimal Feedamount { get; set; }

    public string? Notes { get; set; }

    public virtual ICollection<Nutrition> Nutritions { get; set; } = new List<Nutrition>();
}
