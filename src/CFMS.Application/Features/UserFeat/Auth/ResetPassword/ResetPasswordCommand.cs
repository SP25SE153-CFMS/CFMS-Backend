using CFMS.Application.Common;
using CFMS.Application.DTOs.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.UserFeat.Auth.ResetPassword
{
    public class ResetPasswordCommand : IRequest<BaseResponse<bool>>
    {
        public string? NewPassword { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? Otp { get; set; }
    }
}
