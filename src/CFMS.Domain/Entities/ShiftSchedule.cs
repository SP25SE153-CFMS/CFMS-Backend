using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public class ShiftSchedule : EntityAudit
{
    public Guid ShiftScheduleId { get; set; }

    public Guid? ShiftId { get; set; }

    public DateOnly? Date { get; set; }

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public virtual Shift? Shift { get; set; }
}
