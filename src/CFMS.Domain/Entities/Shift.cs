using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class Shift : EntityAudit
{
    public Guid ShiftId { get; set; }

    public string? ShiftName { get; set; }

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    public Guid? FarmId { get; set; }

    public virtual ICollection<ShiftSchedule> ShiftSchedules { get; set; } = new List<ShiftSchedule>();
}
