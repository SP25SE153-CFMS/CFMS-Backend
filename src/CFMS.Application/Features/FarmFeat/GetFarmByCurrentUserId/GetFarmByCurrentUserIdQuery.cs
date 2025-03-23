using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.FarmFeat.GetFarmByCurrentUserId
{
    public class GetFarmByCurrentUserIdQuery : IRequest<BaseResponse<Farm>>
    {
        public GetFarmByCurrentUserIdQuery(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; set; }
    }
}
