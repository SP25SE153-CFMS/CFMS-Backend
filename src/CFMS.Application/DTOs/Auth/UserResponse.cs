using CFMS.Domain.Enums.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.DTOs.Auth
{
    public class UserResponse
    {
        public Guid UserId { get; set; }

        public string? FullName { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Mail { get; set; }

        public string? Avatar { get; set; }

        public DateOnly? DateOfBirth { get; set; }

        public DateOnly? StartDate { get; set; }

        public string? Status { get; set; }

        public string? Address { get; set; }

        public string? Cccd { get; set; }

        public string? SystemRole { get; set; }
    }
}
