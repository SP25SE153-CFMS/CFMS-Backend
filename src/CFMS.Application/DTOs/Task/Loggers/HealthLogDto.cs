using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.DTOs.Task.Loggers
{
    public class HealthLogDto
    {
        public Guid? ResourceId { get; set; }

        public string? ResourceName { get; set; }

        public string? ResourceCode { get; set; }

        public string? UnitSpecification { get; set; }

        public decimal? ActualQuantity { get; set; }

        public string? Notes { get; set; }

        public ICollection<CriteriaDto> Criterias { get; set; } = new List<CriteriaDto>();
    }
}
