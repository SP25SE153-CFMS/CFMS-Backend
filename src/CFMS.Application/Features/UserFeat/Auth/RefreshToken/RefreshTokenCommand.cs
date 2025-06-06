﻿using CFMS.Application.Common;
using CFMS.Application.DTOs.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.UserFeat.Auth.RefreshToken
{
    public class RefreshTokenCommand : IRequest<BaseResponse<AuthResponse>>
    {
        public string RefreshToken { get; set; }
    }
}
