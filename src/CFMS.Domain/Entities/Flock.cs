using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public class Flock : EntityAudit
{
    public Guid FlockId { get; set; }

    public int? Quantity { get; set; }

    public string? Name { get; set; }

    public DateTime? StartDate { get; set; }

    public int? Status { get; set; }

    public string? Description { get; set; }

    public DateTime? EndDate { get; set; }

    public double? AvgWeight { get; set; }

    public double? MortalityRate { get; set; }

    public DateTime? LastHealthCheck { get; set; }

    public string? Gender { get; set; }

    public Guid? PurposeId { get; set; }

    public Guid? BreedId { get; set; }

    public Guid? ChickenBatchId { get; set; }

    public virtual SubCategory? Breed { get; set; }

    public virtual ChickenBatch? ChickenBatch { get; set; }

    public virtual SubCategory? Purpose { get; set; }

    public virtual ICollection<FlockNutrition> FlockNutritions { get; set; } = new List<FlockNutrition>();

    public virtual ICollection<HealthLog> HealthLogs { get; set; } = new List<HealthLog>();

    public virtual ICollection<QuantityLog> QuantityLogs { get; set; } = new List<QuantityLog>();

    public virtual ICollection<VaccinationLog> VaccinationLogs { get; set; } = new List<VaccinationLog>();
}
