using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class NutritionPlan : EntityAudit
{
    public Guid NutritionPlanId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual ICollection<ChickenNutrition> ChickenNutritions { get; set; } = new List<ChickenNutrition>();

    public virtual ICollection<FeedSession> FeedSessions { get; set; } = new List<FeedSession>();

    public virtual ICollection<GrowthNutrition> GrowthNutritions { get; set; } = new List<GrowthNutrition>();

    public virtual ICollection<NutritionPlanDetail> NutritionPlanDetails { get; set; } = new List<NutritionPlanDetail>();
}
