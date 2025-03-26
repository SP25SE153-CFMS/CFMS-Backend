using CFMS.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.ShiftFeat.Delete
{
    public class DeleteShiftCommand : IRequest<BaseResponse<bool>>
    {
        public DeleteShiftCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
