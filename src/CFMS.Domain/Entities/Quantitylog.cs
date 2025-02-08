using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class Quantitylog
{
    public Guid Qlogid { get; set; }

    public DateTime Logdate { get; set; }

    public string? Notes { get; set; }

    public int Quantity { get; set; }

    public string Logtype { get; set; } = null!;

    public Guid Flockid { get; set; }

    public Guid? Reasonid { get; set; }

    public List<User> Users { get; set; } = new();

    public virtual Flock Flock { get; set; } = null!;

    public virtual Reason? Reason { get; set; }
}
