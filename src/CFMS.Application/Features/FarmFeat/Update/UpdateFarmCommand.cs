using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.FarmFeat.Update
{
    public class UpdateFarmCommand : IRequest<BaseResponse<bool>>
    {
        public UpdateFarmCommand(Guid farmId, string? farmName, string? farmCode, string? type, string? address, double? area, string? scale, string? phoneNumber, string? website, string? farmImage)
        {
            FarmId = farmId;
            FarmName = farmName;
            FarmCode = farmCode;
            Type = type;
            Address = address;
            Area = area;
            Scale = scale;
            PhoneNumber = phoneNumber;
            Website = website;
            FarmImage = farmImage;
        }

        public Guid FarmId { get; set; }

        public string? FarmName { get; set; }

        public string? FarmCode { get; set; }

        public string? Type { get; set; }

        public string? Address { get; set; }

        public double? Area { get; set; }

        public string? Scale { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Website { get; set; }

        public string? FarmImage { get; set; }
    }
}
