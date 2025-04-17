using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.DTOs.TaskResource
{
    public class TaskResourceRequest
    {
        public Guid ResourceId { get; set; }

        public int? SuppliedQuantity { get; set; }

        public int? ConsumedQuantity { get; set; }

        //public Guid? UnitId { get; set; }
    }
}
