using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.GrowthStageFeat.Update
{
    public class UpdateStageCommand : IRequest<BaseResponse<bool>>
    {
        public UpdateStageCommand(Guid id, string? stageName, Guid? chickenType, int? minAgeWeek, int? maxAgeWeek, string? description)
        {
            Id = id;
            StageName = stageName;
            ChickenType = chickenType;
            MinAgeWeek = minAgeWeek;
            MaxAgeWeek = maxAgeWeek;
            Description = description;
        }

        public Guid Id { get; set; }

        public string? StageName { get; set; }

        public Guid? ChickenType { get; set; }

        public int? MinAgeWeek { get; set; }

        public int? MaxAgeWeek { get; set; }

        public string? Description { get; set; }
    }
}
