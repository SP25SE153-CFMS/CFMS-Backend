using CFMS.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.HarvestProductFeat.Delete
{
    public class DeleteHarvestProductCommand : IRequest<BaseResponse<bool>>
    {
        public DeleteHarvestProductCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
