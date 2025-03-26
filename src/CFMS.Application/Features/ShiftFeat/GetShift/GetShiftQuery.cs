using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.ShiftFeat.GetShift
{
    public class GetShiftQuery : IRequest<BaseResponse<Shift>>
    {
        public GetShiftQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
