using CFMS.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.RequestFeat.Delete
{
    public class DeleteRequestCommand : IRequest<BaseResponse<bool>>
    {
        public DeleteRequestCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
