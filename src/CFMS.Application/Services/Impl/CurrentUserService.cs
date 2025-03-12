﻿using CFMS.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

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
    }
}
