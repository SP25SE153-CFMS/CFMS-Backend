using CFMS.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.User.Auth
{
    public class SignInCommand : IRequest<BaseResponse<string>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
