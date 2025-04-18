using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CFMS.Domain.Entities;

public partial class TaskRequest
{
    public Guid TaskRequestId { get; set; }

    public Guid? RequestId { get; set; }

    public string? Title  { get; set; }

    public int? Priority { get; set; }

    public string? Description { get; set; }

    public string[]? ImageUrl { get; set; }

    [JsonIgnore]
    public virtual Request? Request { get; set; }
}
