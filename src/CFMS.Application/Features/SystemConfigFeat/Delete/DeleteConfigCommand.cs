using CFMS.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.SystemConfigFeat.Delete
{
    public class DeleteConfigCommand : IRequest<BaseResponse<bool>>
    {
        public DeleteConfigCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
