using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class GrowthBatch : EntityAudit
{
    public Guid GrowthBatchId { get; set; }

    public Guid? ChickenBatchId { get; set; }

    public Guid? GrowthStageId { get; set; }

    //public DateTime? StartDate { get; set; }

    //public DateTime? EndDate { get; set; }

    //public decimal? AvgWeight { get; set; }

    //public decimal? MortalityRate { get; set; }

    //public decimal? FeedConsumption { get; set; }

    //public string? Note { get; set; }

    //public int? Status { get; set; }

    public virtual ChickenBatch? ChickenBatch { get; set; }

    public virtual GrowthStage? GrowthStage { get; set; }
}
