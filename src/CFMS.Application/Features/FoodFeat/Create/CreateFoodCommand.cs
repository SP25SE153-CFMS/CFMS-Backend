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
        public CreateFoodCommand(string? foodCode, string? foodName, string? note, DateTime? productionDate, DateTime? expiryDate, Guid? unitId, Guid? packageId, decimal? packageSize)
        {
            FoodCode = foodCode;
            FoodName = foodName;
            Note = note;
            ProductionDate = productionDate;
            ExpiryDate = expiryDate;
            UnitId = unitId;
            PackageId = packageId;
            PackageSize = packageSize;
        }

        public string? FoodCode { get; set; }

        public string? FoodName { get; set; }

        public string? Note { get; set; }

        public DateTime? ProductionDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public Guid? UnitId { get; set; }

        public Guid? PackageId { get; set; }

        public decimal? PackageSize { get; set; }
    }
}
