using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.GrowthStageFeat.Create
{
    public class CreateStageCommand : IRequest<BaseResponse<bool>>
    {
        public CreateStageCommand(string? stageName, Guid? chickenType, int? minAgeWeek, int? maxAgeWeek, string? description, string stageCode)
        {
            StageName = stageName;
            ChickenType = chickenType;
            MinAgeWeek = minAgeWeek;
            MaxAgeWeek = maxAgeWeek;
            Description = description;
            StageCode = stageCode;
        }

        public string? StageName { get; set; }

        public string StageCode { get; set; }

        public Guid? ChickenType { get; set; }

        public int? MinAgeWeek { get; set; }

        public int? MaxAgeWeek { get; set; }

        public string? Description { get; set; }
    }
}
