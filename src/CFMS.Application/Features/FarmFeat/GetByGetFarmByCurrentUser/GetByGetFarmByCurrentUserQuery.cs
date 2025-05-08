using CFMS.Application.Common;
using CFMS.Application.DTOs.Farm;
using CFMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.FarmFeat.GetByGetFarmByCurrentUser
{
    public class GetByGetFarmByCurrentUserQuery : IRequest<BaseResponse<IEnumerable<FarmWithRoleResponse>>>
    {
    }
}
