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
        public string EmailOrPhone { get; set; }
        public string Method { get; set; }

        public ForgotPasswordCommand(string emailOrPhone, string method)
        {
            EmailOrPhone = emailOrPhone;
            Method = method;
        }
    }
}
