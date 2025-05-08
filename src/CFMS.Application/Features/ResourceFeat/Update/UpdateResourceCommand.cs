using CFMS.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.ResourceFeat.Update
{
    public class UpdateResourceCommand : IRequest<BaseResponse<bool>>
    {
        public UpdateResourceCommand(Guid resourceId, Guid? resourceTypeId, string? description, Guid? unitId, Guid? packageId, decimal? packageSize, Guid? foodId, Guid? equipmentId, Guid? medicineId)
        {
            ResourceId = resourceId;
            ResourceTypeId = resourceTypeId;
            Description = description;
            UnitId = unitId;
            PackageId = packageId;
            PackageSize = packageSize;
            FoodId = foodId;
            EquipmentId = equipmentId;
            MedicineId = medicineId;
        }

        public Guid ResourceId { get; set; }

        public Guid? ResourceTypeId { get; set; }

        public string? Description { get; set; }

        public Guid? UnitId { get; set; }

        public Guid? PackageId { get; set; }

        public decimal? PackageSize { get; set; }

        public Guid? FoodId { get; set; }

        public Guid? EquipmentId { get; set; }

        public Guid? MedicineId { get; set; }
    }
}
