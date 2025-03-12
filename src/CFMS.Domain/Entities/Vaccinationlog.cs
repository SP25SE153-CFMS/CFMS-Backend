using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public class VaccinationLog : EntityAudit
{
    public Guid VLogId { get; set; }

    public Guid? VaccineId { get; set; }

    public Guid? FlockId { get; set; }

    public string? Dosage { get; set; }

    public string? Notes { get; set; }

    public int? Quantity { get; set; }

    public string? Status { get; set; }

    public string? Reaction { get; set; }

    public virtual Flock? Flock { get; set; }

    public virtual ICollection<VaccinationEmployee> VaccinationEmployees { get; set; } = new List<VaccinationEmployee>();

    public virtual Vaccine? Vaccine { get; set; }
}
