using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.DTOs.Request
{
    public class TaskRequestDto
    {
        public Guid? RequestId { get; set; }

        public Guid? FarmId { get; set; }

        public string? Title { get; set; }

        public int? Priority { get; set; }

        public string? Description { get; set; }

        public string[]? ImageUrl { get; set; }
    }
}