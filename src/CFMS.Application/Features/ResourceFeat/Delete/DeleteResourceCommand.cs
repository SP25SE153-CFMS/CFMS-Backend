using CFMS.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.ResourceFeat.Delete
{
    public class DeleteResourceCommand : IRequest<BaseResponse<bool>>
    {
        public DeleteResourceCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
