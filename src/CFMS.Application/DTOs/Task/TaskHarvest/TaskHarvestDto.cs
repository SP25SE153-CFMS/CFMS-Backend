using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.DTOs.Task.TaskHarvest
{
    public class TaskHarvestDto
    {
        public Guid HarvestProductId { get; set; }

        public string? HarvestProductName { get; set; }

        public string? HarvestProductCode { get; set; }

        public string? HarvestProductType { get; set; }

        public string? SpecQuantity { get; set; }

        public string? UnitSpecification { get; set; }
    }
}
