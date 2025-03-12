using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public class Assignment : EntityAudit
{
    public Guid AssignmentId { get; set; }

    public Guid? TaskId { get; set; }

    public Guid? UserId { get; set; }

    public DateTime? AssignedDate { get; set; }

    public DateTime? DeadlineDate { get; set; }

    public DateTime? CompletedDate { get; set; }

    public string? Status { get; set; }

    public string? Note { get; set; }

    public virtual Task? Task { get; set; }

    public virtual User? User { get; set; }
}
