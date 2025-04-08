using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CFMS.Domain.Entities;

public partial class ChickenBatch : EntityAudit
{
    public Guid ChickenBatchId { get; set; }

    public Guid? ChickenCoopId { get; set; }

    public string? ChickenBatchName { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Note { get; set; }

    public int? Status { get; set; }

    public Guid? ChickenId { get; set; }

    [JsonIgnore]
    public virtual ChickenCoop? ChickenCoop { get; set; }

    public virtual Chicken? Chicken { get; set; }

    public virtual ICollection<FeedLog> FeedLogs { get; set; } = new List<FeedLog>();

    public virtual ICollection<GrowthBatch> GrowthBatches { get; set; } = new List<GrowthBatch>();

    public virtual ICollection<HealthLog> HealthLogs { get; set; } = new List<HealthLog>();

    public virtual ICollection<QuantityLog> QuantityLogs { get; set; } = new List<QuantityLog>();

    public virtual ICollection<VaccineLog> VaccineLogs { get; set; } = new List<VaccineLog>();

    public virtual ICollection<ChickenDetail> ChickenDetails { get; set; } = new List<ChickenDetail>();
}
