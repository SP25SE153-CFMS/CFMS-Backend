using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class NutritionPlanDetail : EntityAudit
{
    public Guid NutritionPlanDetailId { get; set; }

    public Guid? NutritionPlanId { get; set; }

    public Guid? FoodId { get; set; }

    public Guid? UnitId { get; set; }

    public decimal? FoodWeight { get; set; }

    public decimal? ConsumptionRate { get; set; }

    public virtual Food? Food { get; set; }

    public virtual NutritionPlan? NutritionPlan { get; set; }

    public virtual SubCategory? Unit { get; set; }
}
