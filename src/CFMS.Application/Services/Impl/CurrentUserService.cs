using CFMS.Domain.Enums.Types;
using CFMS.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Services.Impl
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? GetUserId()
        {
            return _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public string? GetUserRole()
        {
            return _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value;
        }

        public string? GetUserEmail()
        {
            return _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
        }

        public bool? IsOwner()
        {
            return _httpContextAccessor.HttpContext?.User.IsInRole(RoleType.Owner.ToString());
        }

        public bool? IsManager()
        {
            return _httpContextAccessor.HttpContext?.User.IsInRole(RoleType.Manager.ToString());
        }

        public bool? IsUser()
        {
            return _httpContextAccessor.HttpContext?.User.IsInRole(RoleType.User.ToString());
        }
    }
}
