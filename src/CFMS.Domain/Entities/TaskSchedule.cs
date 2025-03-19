using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public class TaskSchedule : EntityAudit
{
    public Guid TaskScheduleId { get; set; }

    public int? Frequency { get; set; }

    public DateTime? NextWorkDate { get; set; }

    public DateTime? LastWorkDate { get; set; }

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
}
