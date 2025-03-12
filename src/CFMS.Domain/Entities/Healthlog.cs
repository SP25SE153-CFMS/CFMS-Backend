using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public class HealthLog : EntityAudit
{
    public Guid HLogId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Notes { get; set; }

    public Guid? FlockId { get; set; }

    public Guid? StaffId { get; set; }

    public string? Location { get; set; }

    public double? Temperature { get; set; }

    public double? Humidity { get; set; }

    public virtual Flock? Flock { get; set; }

    public virtual ICollection<HealthLogDetail> HealthLogDetails { get; set; } = new List<HealthLogDetail>();
}
