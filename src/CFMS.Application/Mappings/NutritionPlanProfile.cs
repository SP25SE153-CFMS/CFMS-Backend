using AutoMapper;
using CFMS.Application.Features.NutritionPlanFeat.Create;
using CFMS.Domain.Entities;

namespace CFMS.Application.Mappings
{
    public class NutritionPlanProfile : Profile
    {
        public NutritionPlanProfile()
        {
            CreateMap<CreateNutritionPlanCommand, NutritionPlan>();
        }
    }
}
