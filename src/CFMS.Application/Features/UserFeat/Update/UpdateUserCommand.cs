using CFMS.Application.Common;
using CFMS.Domain.Enums.Roles;
using CFMS.Domain.Enums.Status;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.UserFeat.Update
{
    public class UpdateUserCommand : IRequest<BaseResponse<bool>>
    {
        public Guid UserId { get; set; }

        public string? FullName { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Mail { get; set; }

        public string? Avatar { get; set; }

        public DateOnly? DateOfBirth { get; set; }

        public DateOnly? StartDate { get; set; }

        public UserStatus? Status { get; set; }

        public string? Address { get; set; }

        public string? Cccd { get; set; }

        public SystemRole? SystemRole { get; set; }

        public string? Password { get; set; }
    }
}
