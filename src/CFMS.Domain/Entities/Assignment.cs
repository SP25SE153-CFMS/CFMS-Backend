﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CFMS.Domain.Entities;

public partial class Assignment : EntityAudit
{
    public Guid AssignmentId { get; set; }

    public Guid? TaskId { get; set; }

    public Guid? AssignedToId { get; set; }

    public DateTime? AssignedDate { get; set; }

    public int? Status { get; set; }

    public string? Note { get; set; }

    public virtual User? AssignedTo { get; set; }

    [JsonIgnore]
    public virtual Task? Task { get; set; }

    //public Guid? ShiftScheduleId { get; set; }

    //public Guid? TaskScheduleId { get; set; }
}
