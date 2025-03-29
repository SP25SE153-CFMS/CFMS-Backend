using CFMS.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.TaskFeat.Delete
{
    public class DeleteTaskCommand : IRequest<BaseResponse<bool>>
    {
        public DeleteTaskCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
