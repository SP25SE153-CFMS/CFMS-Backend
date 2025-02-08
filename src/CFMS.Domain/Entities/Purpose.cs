using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class Purpose
{
    public Guid Purposeid { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Flock> Flocks { get; set; } = new List<Flock>();
}
