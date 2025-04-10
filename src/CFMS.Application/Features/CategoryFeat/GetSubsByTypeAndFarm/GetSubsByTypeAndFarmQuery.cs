using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.GetSubsByTypeAndName
{
    public class GetSubsByTypeAndFarmQuery : IRequest<BaseResponse<IEnumerable<SubCategory>>>
    {
        public string CategoryType { get; set; }

        public Guid? FarmId { get; set; }

        public GetSubsByTypeAndFarmQuery(string categoryType, Guid? farmId)
        {
            CategoryType = categoryType;
            FarmId = farmId;
        }
    }
}
