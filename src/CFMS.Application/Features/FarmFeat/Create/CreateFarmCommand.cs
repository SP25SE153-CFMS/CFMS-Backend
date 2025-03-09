using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Commands.FarmFeat.Create
{
    public class CreateFarmCommand : IRequest<BaseResponse<bool>>
    {
        public CreateFarmCommand(string? farmName, string? farmCode, string? type, string? address, double? area, string? scale, string? phoneNumber, string? website, string? farmImage, Guid? ownerId)
        {
            FarmName = farmName;
            FarmCode = farmCode;
            Type = type;
            Address = address;
            Area = area;
            Scale = scale;
            PhoneNumber = phoneNumber;
            Website = website;
            FarmImage = farmImage;
            OwnerId = ownerId;
        }

        public string? FarmName { get; set; }

        public string? FarmCode { get; set; }

        public string? Type { get; set; }

        public string? Address { get; set; }

        public double? Area { get; set; }

        public string? Scale { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Website { get; set; }

        public string? FarmImage { get; set; }

        public Guid? OwnerId { get; set; }
    }
}
