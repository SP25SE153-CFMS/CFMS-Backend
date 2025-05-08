using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.DTOs.NutritionPlan
{
    public class NutritionPlanDetailDto
    {
        public Guid FoodId { get; set; }
        public Guid UnitId { get; set; }
        public decimal FoodWeight { get; set; }
        //public decimal ConsumptionRate { get; set; }
    }
}
