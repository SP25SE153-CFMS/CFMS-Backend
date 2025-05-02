using CFMS.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.UserFeat.Auth.ForgotPassword
{
    public class ForgotPasswordQuery : IRequest<BaseResponse<bool>>
    {
        public string? Email { get; set; }

        public ForgotPasswordQuery(string? email)
        {
            Email = email;
        }
    }
}
