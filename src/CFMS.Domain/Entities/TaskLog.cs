using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public class TaskLog : EntityAudit
{
    public Guid TaskLogId { get; set; }

    public string? Type { get; set; }

    public Guid? ChickenCoopId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual ChickenCoop? ChickenCoop { get; set; }

    public virtual ICollection<TaskDetail> TaskDetails { get; set; } = new List<TaskDetail>();
}
