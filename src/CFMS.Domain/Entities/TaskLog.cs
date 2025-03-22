using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class TaskLog : EntityAudit
{
    public Guid TaskLogId { get; set; }

    public Guid? TaskId { get; set; }

    public Guid? ChickenCoopId { get; set; }

    public DateTime? CompletedAt { get; set; }

    public string? Note { get; set; }

    public virtual ChickenCoop? ChickenCoop { get; set; }

    public virtual Task? Task { get; set; }
}
