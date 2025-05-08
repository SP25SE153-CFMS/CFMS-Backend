using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CFMS.Domain.Entities;

public partial class ShiftSchedule
{
    public Guid ShiftScheduleId { get; set; }

    public Guid? ShiftId { get; set; }

    public DateOnly? Date { get; set; }

    public Guid? TaskId { get; set; }

    [JsonIgnore]
    public virtual Task? Task { get; set; }
    [JsonIgnore]
    public virtual Shift? Shift { get; set; }
}
