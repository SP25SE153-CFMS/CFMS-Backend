using CFMS.Application.Common;
using CFMS.Application.DTOs.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.UserFeat.Auth.VerifyPassword
{
    public class VerifyPasswordQuery : IRequest<BaseResponse<bool>>
    {
        public string Password { get; set; }
    }
}
