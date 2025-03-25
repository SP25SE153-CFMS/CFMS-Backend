using CFMS.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.ResourceFeat.Create
{
    public class CreateResourceCommand : IRequest<BaseResponse<bool>>
    {
        public string? ResourceType { get; set; }

        public string? Description { get; set; }

        public Guid? UnitId { get; set; }

        public Guid? PackageId { get; set; }

        public decimal? PackageSize { get; set; }
    }
}
