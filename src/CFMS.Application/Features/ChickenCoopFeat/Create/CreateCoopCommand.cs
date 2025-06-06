﻿using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.ChickenCoopFeat.Create
{
    public class CreateCoopCommand : IRequest<BaseResponse<bool>>
    {
        public CreateCoopCommand(string? chickenCoopCode, string? chickenCoopName, int? maxQuantity, int? area, double? density, string? description, Guid areaUnitId, Guid densityUnitId, Guid? purposeId, Guid? breedingAreaId)
        {
            ChickenCoopCode = chickenCoopCode;
            ChickenCoopName = chickenCoopName;
            MaxQuantity = maxQuantity;
            Area = area;
            Density = density;
            Description = description;
            AreaUnitId = areaUnitId;
            DensityUnitId = densityUnitId;
            PurposeId = purposeId;
            BreedingAreaId = breedingAreaId;
        }

        public string? ChickenCoopCode { get; set; }

        public string? ChickenCoopName { get; set; }

        public int? MaxQuantity { get; set; }

        public int? Area { get; set; }

        public double? Density { get; set; }

        public string? Description { get; set; }

        public Guid AreaUnitId { get; set; }

        public Guid DensityUnitId { get; set; }

        public Guid? PurposeId { get; set; }

        public Guid? BreedingAreaId { get; set; }
    }
}
