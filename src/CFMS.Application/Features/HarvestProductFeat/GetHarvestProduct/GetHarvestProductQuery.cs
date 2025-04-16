using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.HarvestProductFeat.GetHarvestProduct
{
    public class GetHarvestProductQuery : IRequest<BaseResponse<HarvestProduct>>
    {
        public GetHarvestProductQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
