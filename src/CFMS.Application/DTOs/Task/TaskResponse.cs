using CFMS.Application.DTOs.Task.Assignment;
using CFMS.Application.DTOs.Task.Loggers;
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

        public SubCategory? TaskType { get; set; }

        public ShiftScheduleDto? ShiftSchedule { get; set; }

        public TaskLocationDto? TaskLocation { get; set; }

        public ICollection<AssignmentDto> Assignments { get; set; } = new List<AssignmentDto>();

        public ICollection<TaskResourceDto> TaskResources { get; set; } = new List<TaskResourceDto>();

        public ICollection<FeedLogDto> FeedLogs { get; set; } = new List<FeedLogDto>();
    }
}
