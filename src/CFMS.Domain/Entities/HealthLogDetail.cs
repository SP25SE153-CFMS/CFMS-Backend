using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CFMS.Domain.Entities;

public partial class HealthLogDetail
{
    public Guid HealthLogDetailId { get; set; }

    public Guid? HealthLogId { get; set; }

    public Guid? CriteriaId { get; set; }

    public string? Result { get; set; }

    [JsonIgnore]
    public virtual SubCategory? Criteria { get; set; }

    [JsonIgnore]
    public virtual HealthLog? HealthLog { get; set; }
}
