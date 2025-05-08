using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CFMS.Domain.Entities;

public partial class GrowthStage : EntityAudit
{
    public Guid GrowthStageId { get; set; }

    public string? StageName { get; set; }

    public string? StageCode { get; set; }

    public Guid? ChickenType { get; set; }

    public int? MinAgeWeek { get; set; }

    public int? MaxAgeWeek { get; set; }

    public string? Description { get; set; }

    public int OrderNum { get; set; }

    public Guid FarmId { get; set; }

    public Guid? NutritionPlanId { get; set; }

    public virtual NutritionPlan? NutritionPlan { get; set; }

    public virtual SubCategory? ChickenTypeNavigation { get; set; }

    [JsonIgnore]
    public virtual ICollection<GrowthBatch> GrowthBatches { get; set; } = new List<GrowthBatch>();

    [JsonIgnore]
    public virtual ICollection<ChickenBatch> ChickenBatches { get; set; } = new List<ChickenBatch>();
}
