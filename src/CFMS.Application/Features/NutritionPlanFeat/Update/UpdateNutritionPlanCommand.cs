using CFMS.Application.Common;
using CFMS.Application.DTOs.FeedSession;
using CFMS.Application.DTOs.NutritionPlan;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.NutritionPlanFeat.Update
{
    public class UpdateNutritionPlanCommand : IRequest<BaseResponse<bool>>
    {
        public UpdateNutritionPlanCommand(Guid nutritionPlanId, string? name, string? description, List<NutritionPlanDetailUpdateDto>? nutritionPlanDetails, List<FeedSessionUpdateDto>? feedSessions)
        {
            NutritionPlanId = nutritionPlanId;
            Name = name;
            Description = description;
            NutritionPlanDetails = nutritionPlanDetails;
            FeedSessions = feedSessions;
        }

        public Guid NutritionPlanId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public List<NutritionPlanDetailUpdateDto>? NutritionPlanDetails { get; set; }

        public List<FeedSessionUpdateDto>? FeedSessions { get; set; }
    }
}
