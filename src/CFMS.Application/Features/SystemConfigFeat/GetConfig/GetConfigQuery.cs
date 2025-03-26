using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.SystemConfigFeat.GetConfig
{
    public class GetConfigQuery : IRequest<BaseResponse<SystemConfig>>
    {
        public GetConfigQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
