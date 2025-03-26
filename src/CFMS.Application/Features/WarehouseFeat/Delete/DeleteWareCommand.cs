using CFMS.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.WarehouseFeat.Delete
{
    public class DeleteWareCommand : IRequest<BaseResponse<bool>>
    {
        public DeleteWareCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
