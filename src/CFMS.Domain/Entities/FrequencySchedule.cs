using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class FrequencySchedule : EntityAudit
{
    public Guid FrequencyScheduleId { get; set; }

    public int? Frequency { get; set; }

    public Guid? TaskId { get; set; }

    public Guid? TimeUnitId { get; set; }

    public DateTime? StartWorkDate { get; set; }

    public DateTime? EndWorkDate { get; set; }

    public virtual Task? Task { get; set; }

    public virtual SubCategory? TimeUnit { get; set; }
}
