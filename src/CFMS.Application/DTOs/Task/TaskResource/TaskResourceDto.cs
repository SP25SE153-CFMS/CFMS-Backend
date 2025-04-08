using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.DTOs.Task.TaskResource
{
    public class TaskResourceDto
    {
        public Guid ResourceId { get; set; }

        public string? ResourceName { get; set; }

        public string? ResourceType { get; set; }

        public int? Quantity { get; set; }
    }
}
