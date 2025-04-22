using CFMS.Application.Common;
using CFMS.Application.DTOs.FarmEmployee;
using CFMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.FarmFeat.GetFarmEmployee
{
    public class GetFarmEmployeeQuery : IRequest<BaseResponse<FarmEmployeeResponse>>
    {
        public GetFarmEmployeeQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
