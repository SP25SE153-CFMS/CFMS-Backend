using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.DTOs.Auth
{
    public class CurrentUserResponse
    {
        public Guid UserId { get; set; }

        public string? FullName { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Mail { get; set; }

        public string? Avatar { get; set; }

        public DateOnly? DateOfBirth { get; set; }

        public int? Status { get; set; }

        public string? Address { get; set; }

        public string? Cccd { get; set; }

        public int? SystemRole { get; set; }
    }
}
