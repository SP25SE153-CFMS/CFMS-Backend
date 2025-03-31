using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.RequestFeat.GetRequest
{
    public class GetRequestQuery : IRequest<BaseResponse<Request>>
    {
        public GetRequestQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
