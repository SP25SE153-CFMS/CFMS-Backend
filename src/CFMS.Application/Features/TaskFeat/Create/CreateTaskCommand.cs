using CFMS.Application.Common;
using CFMS.Application.DTOs.TaskResource;
using MediatR;

namespace CFMS.Application.Features.TaskFeat.Create
{
    public class CreateTaskCommand : IRequest<BaseResponse<bool>>
    {
        public CreateTaskCommand(string? taskName, Guid? taskTypeId, string? description, int? isHavest, int? status, int? frequency, Guid? timeUnitId, DateTime startWorkDate, DateTime endWorkDate, Guid[] shiftIds, string locationType, Guid locationId)
        {
            TaskName = taskName;
            TaskTypeId = taskTypeId;
            Description = description;
            IsHavest = isHavest;
            Status = status;
            Frequency = frequency;
            TimeUnitId = timeUnitId;
            StartWorkDate = startWorkDate;
            EndWorkDate = endWorkDate;
            ShiftIds = shiftIds;
            LocationType = locationType;
            LocationId = locationId;
        }

        public string? TaskName { get; set; }

        public Guid? TaskTypeId { get; set; }

        public string? Description { get; set; }

        public int? IsHavest { get; set; }

        public int? Status { get; set; }

        //Frequency
        public int? Frequency { get; set; }

        public Guid? TimeUnitId { get; set; }

        public DateTime StartWorkDate { get; set; }

        public DateTime EndWorkDate { get; set; }

        //Shift
        public Guid[] ShiftIds { get; set; }

        //Location
        public string LocationType { get; set; }

        public Guid LocationId { get; set; }

        //Resource
        public IEnumerable<TaskResourceRequest> TaskResources { get; set; }
    }
}
