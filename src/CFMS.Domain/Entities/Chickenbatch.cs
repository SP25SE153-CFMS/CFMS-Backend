using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public class ChickenBatch : EntityAudit
{
    public Guid ChickenBatchId { get; set; }

    public Guid? ChickenCoopId { get; set; }

    public string? ChickenBatchName { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Note { get; set; }

    public int? Status { get; set; }

    public virtual ChickenCoop? ChickenCoop { get; set; }

    public virtual ICollection<Chicken> Chickens { get; set; } = new List<Chicken>();

    public virtual ICollection<FeedLog> FeedLogs { get; set; } = new List<FeedLog>();

    public virtual ICollection<GrowthBatch> GrowthBatches { get; set; } = new List<GrowthBatch>();

    public virtual ICollection<HealthLog> HealthLogs { get; set; } = new List<HealthLog>();

    public virtual ICollection<QuantityLog> QuantityLogs { get; set; } = new List<QuantityLog>();

    public virtual ICollection<VaccineLog> VaccineLogs { get; set; } = new List<VaccineLog>();
}
