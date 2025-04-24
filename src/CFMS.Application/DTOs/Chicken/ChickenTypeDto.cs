using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.DTOs.Chicken
{
    public class ChickenTypeGroupDto
    {
        public ChickenTypeDto ChickenType { get; set; }
        public List<ChickenDto> Chickens { get; set; }
    }

    public class ChickenTypeDto
    {
        public Guid SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public string Description { get; set; }
    }

    public class ChickenDto
    {
        public Guid ChickenId { get; set; }
        public string ChickenCode { get; set; }
        public string ChickenName { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
    }
}
