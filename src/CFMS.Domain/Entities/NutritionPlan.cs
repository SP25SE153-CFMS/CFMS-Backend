using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CFMS.Domain.Entities;

public partial class NutritionPlan : EntityAudit
{
    public Guid NutritionPlanId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public Guid? FarmId { get; set; }

    //public DateTime? StartDate { get; set; }

    //public DateTime? EndDate { get; set; }

    public virtual ICollection<FeedSession> FeedSessions { get; set; } = new List<FeedSession>();

    [JsonIgnore]
    public virtual ICollection<GrowthStage> GrowthStages { get; set; } = new List<GrowthStage>();

    public virtual ICollection<NutritionPlanDetail> NutritionPlanDetails { get; set; } = new List<NutritionPlanDetail>();
}
