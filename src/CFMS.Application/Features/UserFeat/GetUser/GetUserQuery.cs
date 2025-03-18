using CFMS.Application.Common;
using CFMS.Application.DTOs.Auth;
using CFMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.UserFeat.GetUser
{
    public class GetUserQuery : IRequest<BaseResponse<UserResponse>>
    {
        public GetUserQuery(Guid id)
        {
            UserId = id;
        }

        public Guid UserId { get; set; }
    }
}
