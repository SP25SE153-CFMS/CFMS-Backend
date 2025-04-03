using CFMS.Application.Common;
using CFMS.Application.DTOs.Farm;
using CFMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.FarmFeat.GetFarmByCurrentEmployeeByFarmId
{
    public class GetFarmByCurrentEmployeeByFarmIdQuery : IRequest<BaseResponse<FarmResponse>>
    {
        public GetFarmByCurrentEmployeeByFarmIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
