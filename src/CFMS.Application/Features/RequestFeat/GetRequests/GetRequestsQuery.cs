using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.RequestFeat.GetRequests
{
    public class GetRequestsQuery : IRequest<BaseResponse<IEnumerable<Request>>>
    {
        public GetRequestsQuery() { }
    }
}
