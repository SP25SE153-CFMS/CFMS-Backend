using CFMS.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.EquipmentFeat.Update
{
    public class UpdateEquipmentCommand : IRequest<BaseResponse<bool>>
    {
        public UpdateEquipmentCommand(Guid equipmentId, string? equipmentCode, string? equipmentName, Guid? materialId, string? usage, int? warranty, decimal? size, Guid? sizeUnitId, decimal? weight, Guid? weightUnitId, DateTime? purchaseDate)
        {
            EquipmentId = equipmentId;
            EquipmentCode = equipmentCode;
            EquipmentName = equipmentName;
            MaterialId = materialId;
            Usage = usage;
            Warranty = warranty;
            Size = size;
            SizeUnitId = sizeUnitId;
            Weight = weight;
            WeightUnitId = weightUnitId;
            PurchaseDate = purchaseDate;
        }

        public Guid EquipmentId { get; set; }

        public string? EquipmentCode { get; set; }

        public string? EquipmentName { get; set; }

        public Guid? MaterialId { get; set; }

        public string? Usage { get; set; }

        public int? Warranty { get; set; }

        public decimal? Size { get; set; }

        public Guid? SizeUnitId { get; set; }

        public decimal? Weight { get; set; }

        public Guid? WeightUnitId { get; set; }

        public DateTime? PurchaseDate { get; set; }

    }
}
