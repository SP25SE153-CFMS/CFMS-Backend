using CFMS.Application.Common;
using CFMS.Application.DTOs.TaskResource;
using MediatR;

namespace CFMS.Application.Features.TaskFeat.Create
{
    public class CreateTaskCommand : IRequest<BaseResponse<bool>>
    {
        public CreateTaskCommand(string? taskName, Guid? taskTypeId, string? description, int? isHarvest, Guid farmId, DateTime[]? startWorkDate, Guid[]? shiftIds, string? locationType, Guid locationId, IEnumerable<TaskResourceRequest>? taskResources)
        {
            TaskName = taskName;
            TaskTypeId = taskTypeId;
            Description = description;
            IsHarvest = isHarvest;
            FarmId = farmId;
            StartWorkDate = startWorkDate;
            ShiftIds = shiftIds;
            LocationType = locationType;
            LocationId = locationId;
            TaskResources = taskResources;
        }

        public string? TaskName { get; set; }

        public Guid? TaskTypeId { get; set; }

        public string? Description { get; set; }

        public int? IsHarvest { get; set; }

        //public int? Status { get; set; }

        public Guid FarmId { get; set; }

        //Time
        public DateTime[]? StartWorkDate { get; set; }

        //Shift
        public Guid[]? ShiftIds { get; set; }

        //Location
        public string? LocationType { get; set; }

        public Guid LocationId { get; set; }

        //Resource
        public IEnumerable<TaskResourceRequest>? TaskResources { get; set; }
    }
}
