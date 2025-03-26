using CFMS.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.FoodFeat.Update
{
    public class UpdateFoodCommand : IRequest<BaseResponse<bool>>
    {
        public UpdateFoodCommand(Guid foodId, string? foodCode, string? foodName, string? note, DateTime? productionDate, DateTime? expiryDate)
        {
            FoodId = foodId;
            FoodCode = foodCode;
            FoodName = foodName;
            Note = note;
            ProductionDate = productionDate;
            ExpiryDate = expiryDate;
        }

        public Guid FoodId { get; set; }

        public string? FoodCode { get; set; }

        public string? FoodName { get; set; }

        public string? Note { get; set; }

        public DateTime? ProductionDate { get; set; }

        public DateTime? ExpiryDate { get; set; }
    }
}
