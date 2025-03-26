using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.WarehouseFeat.GetWare
{
    public class GetWareQuery : IRequest<BaseResponse<Warehouse>>
    {
        public GetWareQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
