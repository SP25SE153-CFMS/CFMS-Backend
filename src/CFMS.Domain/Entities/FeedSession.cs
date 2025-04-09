using System.Text.Json.Serialization;

namespace CFMS.Domain.Entities;

public partial class FeedSession : EntityAudit
{
    public Guid FeedSessionId { get; set; }

    public Guid? NutritionPlanId { get; set; }

    public DateTime? FeedingTime { get; set; }

    public decimal? FeedAmount { get; set; }

    public Guid? UnitId { get; set; }

    public string? Note { get; set; }

    [JsonIgnore]
    public virtual NutritionPlan? NutritionPlan { get; set; }

    public virtual SubCategory? Unit { get; set; }
}
