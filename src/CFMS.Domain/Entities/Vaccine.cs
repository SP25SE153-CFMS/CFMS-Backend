using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public class Vaccine : EntityAudit
{
    public Guid VaccineId { get; set; }

    public string? Name { get; set; }

    public string? Notes { get; set; }

    public DateTime? ProductionDate { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public string? Dosage { get; set; }

    public string? Instructions { get; set; }

    public string? BatchNumber { get; set; }

    public Guid? SupplierId { get; set; }

    public Guid? DiseaseId { get; set; }

    public Guid ProductId { get; set; }

    public virtual SubCategory? Disease { get; set; }

    public virtual Product? Product { get; set; }

    public virtual SubCategory? Supplier { get; set; }

    public virtual ICollection<VaccinationLog> VaccinationLogs { get; set; } = new List<VaccinationLog>();
}
