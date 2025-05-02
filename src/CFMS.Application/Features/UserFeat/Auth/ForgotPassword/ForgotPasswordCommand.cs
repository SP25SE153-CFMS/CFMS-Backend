using CFMS.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.UserFeat.Auth.ForgotPassword
{
    public class ForgotPasswordCommand : IRequest<BaseResponse<bool>>
    {
        public string? Email { get; set; }

        public ForgotPasswordCommand(string? email)
        {
            Email = email;
        }
    }
}
