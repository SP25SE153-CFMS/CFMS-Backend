﻿using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.GrowthStageFeat.AddNutritionPlan
{
    public class AddGrowthStageNutritionPlanCommand : IRequest<BaseResponse<bool>>
    {
        public AddGrowthStageNutritionPlanCommand(Guid nutritionPlanId, Guid growthStageId)
        {
            NutritionPlanId = nutritionPlanId;
            GrowthStageId = growthStageId;
        }

        public Guid NutritionPlanId { get; set; }

        public Guid GrowthStageId { get; set; }
    }
}
