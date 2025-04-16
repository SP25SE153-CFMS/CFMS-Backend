using CFMS.Domain.Entities;
using System.Text.Json.Serialization;

namespace CFMS.Application.DTOs.Task.TaskLocation
{
    public class TaskLocationDto
    {
        public Guid TaskLocationId { get; set; }

        public Guid? TaskId { get; set; }

        public string? LocationType { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Guid? CoopId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Guid? WareId { get; set; }

        [JsonIgnore]
        public virtual ChickenCoop Location { get; set; } = null!;

        [JsonIgnore]
        public virtual CFMS.Domain.Entities.Warehouse LocationNavigation { get; set; } = null!;
    }
}
