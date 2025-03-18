using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public class GrowthNutrition : EntityAudit
{
    public Guid GrowthNutritionId { get; set; }

    public Guid? NutritionPlanId { get; set; }

    public Guid? GrowthStageId { get; set; }

    public virtual GrowthStage? GrowthStage { get; set; }

    public virtual NutritionPlan? NutritionPlan { get; set; }
}
