using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CFMS.Domain.Entities;

public partial class NutritionPlanDetail
{
    public Guid NutritionPlanDetailId { get; set; }

    public Guid? NutritionPlanId { get; set; }

    public Guid? FoodId { get; set; }

    public Guid? UnitId { get; set; }

    public decimal? FoodWeight { get; set; }

    public virtual Food? Food { get; set; }

    [JsonIgnore]
    public virtual NutritionPlan? NutritionPlan { get; set; }

    public virtual SubCategory? Unit { get; set; }
}
