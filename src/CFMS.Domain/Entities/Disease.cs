using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class Disease
{
    public Guid Diseaseid { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Diseasetype { get; set; }

    public string? Cause { get; set; }

    public virtual ICollection<Vaccine> Vaccines { get; set; } = new List<Vaccine>();
}
