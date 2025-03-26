using CFMS.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.WarehouseFeat.Create
{
    public class CreateWareCommand : IRequest<BaseResponse<bool>>
    {
        public CreateWareCommand(Guid? farmId, Guid? resourceTypeId, string? warehouseName, int? maxQuantity, decimal? maxWeight, int? currentQuantity, decimal? currentWeight, string? description, int? status)
        {
            FarmId = farmId;
            ResourceTypeId = resourceTypeId;
            WarehouseName = warehouseName;
            MaxQuantity = maxQuantity;
            MaxWeight = maxWeight;
            CurrentQuantity = currentQuantity;
            CurrentWeight = currentWeight;
            Description = description;
            Status = status;
        }

        public Guid? FarmId { get; set; }

        public Guid? ResourceTypeId { get; set; }

        public string? WarehouseName { get; set; }

        public int? MaxQuantity { get; set; }

        public decimal? MaxWeight { get; set; }

        public int? CurrentQuantity { get; set; }

        public decimal? CurrentWeight { get; set; }

        public string? Description { get; set; }

        public int? Status { get; set; }
    }
}
