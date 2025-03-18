using CFMS.Application.Common;
using CFMS.Application.DTOs.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.UserFeat.Auth
{
    public class GetCurrentUserQuery : IRequest<BaseResponse<CurrentUserResponse>>
    {
        public GetCurrentUserQuery()
        {
        }
    }
}
