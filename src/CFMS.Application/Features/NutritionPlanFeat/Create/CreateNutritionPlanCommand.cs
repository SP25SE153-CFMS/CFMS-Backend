﻿using CFMS.Application.Common;
using CFMS.Application.DTOs.FeedSession;
using CFMS.Application.DTOs.NutritionPlan;
using MediatR;

namespace CFMS.Application.Features.NutritionPlanFeat.Create
{
    public class CreateNutritionPlanCommand : IRequest<BaseResponse<bool>>
    {
        public CreateNutritionPlanCommand(string? name, string? description, List<NutritionPlanDetailDto> nutritionPlanDetails, List<FeedSessionRequest>? feedSessions, Guid farmId)
        {
            Name = name;
            Description = description;
            NutritionPlanDetails = nutritionPlanDetails;
            FeedSessions = feedSessions;
            FarmId = farmId;
        }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public Guid FarmId { get; set; }

        public List<NutritionPlanDetailDto>? NutritionPlanDetails { get; set; }

        public List<FeedSessionRequest>? FeedSessions { get; set; }
    }
}
