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

        public string? ResourceCode { get; set; }

        public string? ResourceType { get; set; }

        public string? SpecQuantity { get; set; }

        public string? UnitSpecification { get; set; }

        public Guid? SupplierId { get; set; }
    }
}
