﻿using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.FarmFeat.Update
{
    public class UpdateFarmCommand : IRequest<BaseResponse<bool>>
    {
        public UpdateFarmCommand(Guid farmId, string? farmName, string? farmCode, string? address, decimal? area, int? scale, string? phoneNumber, string? website, string? imageUrl, decimal? longitude, decimal? latitude)
        {
            FarmId = farmId;
            FarmName = farmName;
            FarmCode = farmCode;
            Address = address;
            Area = area;
            Scale = scale;
            PhoneNumber = phoneNumber;
            Website = website;
            ImageUrl = imageUrl;
            Longitude = longitude;
            Latitude = latitude;
        }

        public Guid FarmId { get; set; }

        public string? FarmName { get; set; }

        public string? FarmCode { get; set; }

        public string? Address { get; set; }

        public decimal? Area { get; set; }

        public decimal? Longitude { get; set; }

        public decimal? Latitude { get; set; }

        public int? Scale { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Website { get; set; }

        public string? ImageUrl { get; set; }
    }
}
