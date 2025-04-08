using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.DTOs.Task.Assignment
{
    public class AssignmentDto
    {
        public Guid AssignmentId { get; set; }

        public string? AssignedTo { get; set; }

        public DateTime? AssignedDate { get; set; }

        public int? Status { get; set; }

        public string? Note { get; set; }
    }
}
