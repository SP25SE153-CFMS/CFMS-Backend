using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class FeedSchedule
{
    public Guid FeedScheduleId { get; set; }

    public DateTime? FeedTime { get; set; }

    public double? FeedAmount { get; set; }

    public string? Notes { get; set; }

    public virtual ICollection<Nutrition> Nutritions { get; set; } = new List<Nutrition>();
}
