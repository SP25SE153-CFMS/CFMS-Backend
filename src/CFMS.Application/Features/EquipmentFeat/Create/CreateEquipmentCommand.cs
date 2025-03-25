using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.EquipmentFeat.Create
{
    public class CreateEquipmentCommand : IRequest<BaseResponse<bool>>
    {
        public CreateEquipmentCommand(string? equipmentCode, string? equipmentName, string? material, string? usage, int? warranty, decimal? size, Guid? sizeUnitId, decimal? weight, Guid? weightUnitId, DateTime? purchaseDate)
        {
            EquipmentCode = equipmentCode;
            EquipmentName = equipmentName;
            Material = material;
            Usage = usage;
            Warranty = warranty;
            Size = size;
            SizeUnitId = sizeUnitId;
            Weight = weight;
            WeightUnitId = weightUnitId;
            PurchaseDate = purchaseDate;
        }

        public string? EquipmentCode { get; set; }

        public string? EquipmentName { get; set; }

        public string? Material { get; set; }

        public string? Usage { get; set; }

        public int? Warranty { get; set; }

        public decimal? Size { get; set; }

        public Guid? SizeUnitId { get; set; }

        public decimal? Weight { get; set; }

        public Guid? WeightUnitId { get; set; }

        public DateTime? PurchaseDate { get; set; }
    }
}
