using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.DTOs.Task
{
    public class TaskDto
    {
        public string? TaskName { get; set; }

        public string? TaskType { get; set; }

        public string? Description { get; set; }

        //public int? IsHavest { get; set; }

        public int? Status { get; set; }
    }
}
