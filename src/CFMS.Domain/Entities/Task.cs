using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class Task : EntityAudit
{
    public Guid TaskId { get; set; }

    public string? TaskName { get; set; }

    public Guid? TaskTypeId { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public virtual ICollection<EvaluatedTarget> EvaluatedTargets { get; set; } = new List<EvaluatedTarget>();

    public virtual ICollection<FeedLog> FeedLogs { get; set; } = new List<FeedLog>();

    public virtual ICollection<HealthLog> HealthLogs { get; set; } = new List<HealthLog>();

    public virtual ICollection<TaskHarvest> TaskHarvests { get; set; } = new List<TaskHarvest>();

    public virtual ICollection<TaskLocation> TaskLocations { get; set; } = new List<TaskLocation>();

    public virtual ICollection<TaskLog> TaskLogs { get; set; } = new List<TaskLog>();

    public virtual SystemConfig TaskNavigation { get; set; } = null!;

    public virtual ICollection<TaskResource> TaskResources { get; set; } = new List<TaskResource>();

    public virtual SubCategory? TaskType { get; set; }

    public virtual ICollection<VaccineLog> VaccineLogs { get; set; } = new List<VaccineLog>();
}
