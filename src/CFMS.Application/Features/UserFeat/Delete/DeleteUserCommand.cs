using CFMS.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.UserFeat.Delete
{
    public class DeleteUserCommand : IRequest<BaseResponse<bool>>
    {
        public DeleteUserCommand(Guid id)
        {
            UserId = id;
        }

        public Guid UserId { get; set; }
    }
}
