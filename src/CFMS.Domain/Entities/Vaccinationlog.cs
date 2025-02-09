using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class Vaccinationlog
{
    public Guid Vlogid { get; set; }

    public DateTime Vaccinationdate { get; set; }

    public decimal Dosage { get; set; }

    public string? Notes { get; set; }

    public int Quantity { get; set; }

    public string Status { get; set; } = null!;

    public Guid Vaccineid { get; set; }

    public Guid Flockid { get; set; }

    public virtual Flock Flock { get; set; } = null!;

    public virtual Vaccine Vaccine { get; set; } = null!;

    public List<User> Users { get; set; } = new List<User>();
}
