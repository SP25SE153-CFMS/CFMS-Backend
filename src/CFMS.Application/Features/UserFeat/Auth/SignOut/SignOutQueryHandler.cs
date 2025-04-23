﻿using CFMS.Application.Common;
using CFMS.Application.Services;
using CFMS.Application.Services.Impl;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.UserFeat.Auth.SignOut
{
    public class SignOutQueryHandler : IRequestHandler<SignOutQuery, BaseResponse<string>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDistributedCache _cache;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUtilityService _utilityService;

        public SignOutQueryHandler(IHttpContextAccessor httpContextAccessor, IDistributedCache cache, IUnitOfWork unitOfWork, ICurrentUserService currentUserService, IUtilityService utilityService)
        {
            _httpContextAccessor = httpContextAccessor;
            _cache = cache;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
            _utilityService = utilityService;
        }

        public async Task<BaseResponse<string>> Handle(SignOutQuery request, CancellationToken cancellationToken)
        {
            var context = _httpContextAccessor.HttpContext;
            if (context == null)
            {
                return BaseResponse<string>.FailureResponse("Có lỗi xảy ra");
            }

            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
            {
                return BaseResponse<string>.FailureResponse("Thiếu Token");
            }

            await _cache.SetStringAsync($"blacklist_{token}", "true", new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            });

            var user = _currentUserService.GetUserId();
            var requiredRevokedToken = _unitOfWork.RevokedTokenRepository.Get(filter: x => user.Equals(x.UserId.ToString()) && x.RevokedAt == null).FirstOrDefault();
            requiredRevokedToken.RevokedAt = DateTime.UtcNow.ToLocalTime();
            _unitOfWork.RevokedTokenRepository.Update(requiredRevokedToken);
            await _unitOfWork.SaveChangesAsync();

            return BaseResponse<string>.SuccessResponse("Đăng xuất thành công" );
        }
    }
}
