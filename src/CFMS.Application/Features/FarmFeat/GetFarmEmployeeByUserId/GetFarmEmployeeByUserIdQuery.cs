using CFMS.Application.Common;
using CFMS.Application.DTOs.FarmEmployee;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.FarmFeat.GetFarmEmployeeByUserId
{
    public class GetFarmEmployeeByUserIdQuery : IRequest<BaseResponse<FarmEmployeeResponse>>
    {
        public GetFarmEmployeeByUserIdQuery(Guid userId, Guid farmId)
        {
            UserId = userId;
            FarmId = farmId;
        }

        public Guid UserId { get; set; }
        public Guid FarmId { get; set; }
    }
}
