using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class Reason
{
    public Guid Reasonid { get; set; }

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Quantitylog> Quantitylogs { get; set; } = new List<Quantitylog>();
}
