using CFMS.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.FoodFeat.Create
{
    public class CreateFoodCommand : IRequest<BaseResponse<bool>>
    {
        public CreateFoodCommand(string? foodCode, string? foodName, string? note, DateTime? productionDate, DateTime? expiryDate)
        {
            FoodCode = foodCode;
            FoodName = foodName;
            Note = note;
            ProductionDate = productionDate;
            ExpiryDate = expiryDate;
        }

        public string? FoodCode { get; set; }

        public string? FoodName { get; set; }

        public string? Note { get; set; }

        public DateTime? ProductionDate { get; set; }

        public DateTime? ExpiryDate { get; set; }
    }
}
