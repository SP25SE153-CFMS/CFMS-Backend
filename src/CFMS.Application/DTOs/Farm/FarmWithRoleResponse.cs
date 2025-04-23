using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.DTOs.Farm
{
    public class FarmWithRoleResponse
    {
        public Guid? FarmId { get; set; }

        public string? FarmName { get; set; }

        public string? FarmCode { get; set; }

        public string? Address { get; set; }

        public decimal? Area { get; set; }

        public Guid? AreaUnitId { get; set; }

        public string? AreaUnitName { get; set; }

        public int? Scale { get; set; }

        public decimal? Longitude { get; set; }

        public decimal? Latitude { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Website { get; set; }

        public string? ImageUrl { get; set; }

        public int? FarmRole { get; set; }
    }
}
