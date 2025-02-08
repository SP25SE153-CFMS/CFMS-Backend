using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class Flock
{
    public Guid Flockid { get; set; }

    public string Name { get; set; } = null!;

    public int Quantity { get; set; }

    public DateOnly Startdate { get; set; }

    public DateOnly? Enddate { get; set; }

    public string? Status { get; set; }

    public string? Description { get; set; }

    public Guid Purposeid { get; set; }

    public Guid Breedid { get; set; }

    public virtual Breed Breed { get; set; } = null!;

    public virtual ICollection<Chickenbatch> Chickenbatches { get; set; } = new List<Chickenbatch>();

    public virtual ICollection<Healthlog> Healthlogs { get; set; } = new List<Healthlog>();

    public virtual ICollection<Nutrition> Nutritions { get; set; } = new List<Nutrition>();

    public virtual Purpose Purpose { get; set; } = null!;

    public virtual ICollection<Quantitylog> Quantitylogs { get; set; } = new List<Quantitylog>();

    public virtual ICollection<Vaccinationlog> Vaccinationlogs { get; set; } = new List<Vaccinationlog>();
}
