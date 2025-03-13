using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.DTOs.Category
{
    public class CategoryResponse
    {
        public Guid CategoryId { get; set; }

        public string? CategoryType { get; set; }

        public string? CategoryCode { get; set; }

        public string? Description { get; set; }

        public string? Status { get; set; }
    }
}
