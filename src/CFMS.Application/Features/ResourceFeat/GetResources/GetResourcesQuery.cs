using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.ResourceFeat.GetResources
{
    public class GetResourcesQuery : IRequest<BaseResponse<IEnumerable<Resource>>>
    {
        public GetResourcesQuery() { }
    }
}

