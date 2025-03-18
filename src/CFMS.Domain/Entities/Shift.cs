using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public class Shift : EntityAudit
{
    public Guid ShiftId { get; set; }

    public string? ShiftName { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public virtual ICollection<ShiftSchedule> ShiftSchedules { get; set; } = new List<ShiftSchedule>();
}
