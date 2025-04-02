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

        public int? Quantity { get; set; }
    }
}
