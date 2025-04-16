using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CFMS.Domain.Entities;

public partial class Task : EntityAudit
{
    public Guid TaskId { get; set; }

    public string? TaskName { get; set; }

    public Guid? TaskTypeId { get; set; }

    public string? Description { get; set; }

    public int? IsHavest { get; set; }

    public int? Status { get; set; }

    public DateTime? StartWorkDate { get; set; }

    public DateTime? EndWorkDate { get; set; }

    public Guid? FarmId { get; set; }

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public virtual ICollection<ShiftSchedule> ShiftSchedules { get; set; } = new List<ShiftSchedule>();

    public virtual ICollection<EvaluatedTarget> EvaluatedTargets { get; set; } = new List<EvaluatedTarget>();

    public virtual ICollection<FeedLog> FeedLogs { get; set; } = new List<FeedLog>();

    public virtual ICollection<HealthLog> HealthLogs { get; set; } = new List<HealthLog>();

    public virtual ICollection<TaskHarvest> TaskHarvests { get; set; } = new List<TaskHarvest>();

    public virtual ICollection<TaskLocation> TaskLocations { get; set; } = new List<TaskLocation>();

    [JsonIgnore]
    public virtual ICollection<TaskLog> TaskLogs { get; set; } = new List<TaskLog>();

    public virtual SystemConfig TaskNavigation { get; set; } = null!;

    public virtual ICollection<TaskResource> TaskResources { get; set; } = new List<TaskResource>();

    public virtual SubCategory? TaskType { get; set; }

    public virtual ICollection<VaccineLog> VaccineLogs { get; set; } = new List<VaccineLog>();
}
