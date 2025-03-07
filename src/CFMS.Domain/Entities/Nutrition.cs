using System;
using System.Collections.Generic;

namespace CFMS.Infrastructure.Persistence;

public partial class Nutrition
{
    public Guid NutritionId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? TargetAudience { get; set; }

    public string? DevelopmentStage { get; set; }

    public Guid? FoodId { get; set; }

    public Guid? FeedScheduleId { get; set; }

    public virtual FeedSchedule? FeedSchedule { get; set; }

    public virtual ICollection<FlockNutrition> FlockNutritions { get; set; } = new List<FlockNutrition>();

    public virtual Food? Food { get; set; }
}
