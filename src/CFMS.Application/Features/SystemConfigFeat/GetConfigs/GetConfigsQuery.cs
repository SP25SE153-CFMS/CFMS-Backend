using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.SystemConfigFeat.GetConfigs
{
    public class GetConfigsQuery : IRequest<BaseResponse<IEnumerable<SystemConfig>>>
    {
        public GetConfigsQuery() { }
    }
}
