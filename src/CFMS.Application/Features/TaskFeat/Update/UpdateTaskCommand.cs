using CFMS.Application.Common;
using CFMS.Application.DTOs.Assignment;
using CFMS.Application.DTOs.TaskResource;
using MediatR;

namespace CFMS.Application.Features.TaskFeat.Update
{
    public class UpdateTaskCommand : IRequest<BaseResponse<bool>>
    {
        public UpdateTaskCommand(Guid taskId, string? taskName, Guid? taskTypeId, string? description, Guid farmId, DateTime? startWorkDate, Guid? shiftId, string? locationType, Guid locationId, IEnumerable<TaskResourceRequest>? taskResources)
        {
            TaskId = taskId;
            TaskName = taskName;
            TaskTypeId = taskTypeId;
            Description = description;
            FarmId = farmId;
            StartWorkDate = startWorkDate;
            ShiftId = shiftId;
            LocationType = locationType;
            LocationId = locationId;
            TaskResources = taskResources;
        }

        public Guid TaskId { get; set; }    

        public string? TaskName { get; set; }

        public Guid? TaskTypeId { get; set; }

        public string? Description { get; set; }

        //public int? IsHarvest { get; set; }

        //public int? Status { get; set; }

        public Guid FarmId { get; set; }

        //Time
        public DateTime? StartWorkDate { get; set; }

        //Shift
        public Guid? ShiftId { get; set; }

        //Location
        public string? LocationType { get; set; }

        public Guid LocationId { get; set; }

        //Resource
        public IEnumerable<TaskResourceRequest>? TaskResources { get; set; }

        public IEnumerable<AssignmentRequest>? AssignedTos { get; set; }

        public DateTime? AssignedDate { get; set; }

        public string? Note { get; set; }
    }
}
