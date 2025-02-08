using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class Healthlog
{
    public Guid Hlogid { get; set; }

    public DateTime Logdate { get; set; }

    public string? Notes { get; set; }

    public Guid Flockid { get; set; }

    public virtual Flock Flock { get; set; } = null!;

    public List<User> Users { get; set; } = new();
}
