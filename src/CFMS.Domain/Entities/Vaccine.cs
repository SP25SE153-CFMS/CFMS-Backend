using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class Vaccine
{
    public Guid Vaccineid { get; set; }

    public string Name { get; set; } = null!;

    public string? Notes { get; set; }

    public DateOnly Productiondate { get; set; }

    public DateOnly Expirydate { get; set; }

    public string Dosage { get; set; } = null!;

    public string? Instructions { get; set; }

    public string Batchnumber { get; set; } = null!;

    public Guid Supplierid { get; set; }

    public Guid Diseaseid { get; set; }

    public virtual Disease Disease { get; set; } = null!;

    public virtual Supplier Supplier { get; set; } = null!;

    public virtual ICollection<Vaccinationlog> Vaccinationlogs { get; set; } = new List<Vaccinationlog>();
}
