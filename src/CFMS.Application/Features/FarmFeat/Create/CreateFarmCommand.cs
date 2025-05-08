using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.FarmFeat.Create
{
    public class CreateFarmCommand : IRequest<BaseResponse<bool>>
    {
        public CreateFarmCommand(string? farmName, string? farmCode, string? address, decimal? area, int? scale, string? phoneNumber, string? website, string? imageUrl, decimal? longitude, decimal? latitude, Guid? areaUnitId)
        {
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
            AreaUnitId = areaUnitId;
        }

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

        public Guid? AreaUnitId { get; set; }
    }
}
