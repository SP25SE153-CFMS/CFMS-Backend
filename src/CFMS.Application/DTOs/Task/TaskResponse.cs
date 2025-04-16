using CFMS.Application.DTOs.Task.Assignment;
using CFMS.Application.DTOs.Task.ShiftSchedule;
using CFMS.Application.DTOs.Task.TaskLocation;
using CFMS.Application.DTOs.Task.TaskResource;
using CFMS.Domain.Entities;

namespace CFMS.Application.DTOs.Task
{
    public class TaskResponse
    {
        public Guid TaskId { get; set; }

        public string? TaskName { get; set; }

        public Guid? TaskTypeId { get; set; }

        public string? Description { get; set; }

        public int? IsHavest { get; set; }

        public int? Status { get; set; }

        public DateTime? StartWorkDate { get; set; }

        public DateTime? EndWorkDate { get; set; }

        public Guid? FarmId { get; set; }

        public virtual SubCategory? TaskType { get; set; }

        public virtual ShiftScheduleDto ShiftSchedule { get; set; }

        public virtual TaskLocationDto TaskLocation { get; set; }

        public virtual ICollection<AssignmentDto> Assignments { get; set; } = new List<AssignmentDto>();

        public virtual ICollection<TaskResourceDto> TaskResources { get; set; } = new List<TaskResourceDto>();
    }
}
