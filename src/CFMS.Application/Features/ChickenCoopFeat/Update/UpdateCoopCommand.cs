﻿using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.ChickenCoopFeat.Update
{
    public class UpdateCoopCommand : IRequest<BaseResponse<bool>>
    {
        public UpdateCoopCommand(Guid chickenCoopId, string? chickenCoopCode, string? chickenCoopName, int? maxQuantity, int? area, decimal? density, int? currentQuantity, string? description, int? status, Guid? purposeId, Guid? breedingAreaId)
        {
            ChickenCoopId = chickenCoopId;
            ChickenCoopCode = chickenCoopCode;
            ChickenCoopName = chickenCoopName;
            MaxQuantity = maxQuantity;
            Area = area;
            Density = density;
            CurrentQuantity = currentQuantity;
            Description = description;
            Status = status;
            PurposeId = purposeId;
            BreedingAreaId = breedingAreaId;
        }

        public Guid ChickenCoopId { get; set; }

        public string? ChickenCoopCode { get; set; }

        public string? ChickenCoopName { get; set; }

        public int? MaxQuantity { get; set; }

        public int? Area { get; set; }

        public decimal? Density { get; set; }

        public int? CurrentQuantity { get; set; }

        public string? Description { get; set; }

        public int? Status { get; set; }

        public Guid? PurposeId { get; set; }

        public Guid? BreedingAreaId { get; set; }
    }
}
