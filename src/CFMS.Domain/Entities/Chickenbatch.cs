using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class Chickenbatch
{
    public Guid Chickenbatchid { get; set; }

    public int Numberofchicken { get; set; }

    public DateOnly Startdate { get; set; }

    public DateOnly? Enddate { get; set; }

    public string Status { get; set; } = null!;

    public string? Note { get; set; }

    public Guid Breadingareaid { get; set; }

    public Guid Flockid { get; set; }

    public virtual Breadingarea Breadingarea { get; set; } = null!;

    public virtual ICollection<Exportedproduct> Exportedproducts { get; set; } = new List<Exportedproduct>();

    public virtual Flock Flock { get; set; } = null!;
}
