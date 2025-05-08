using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.CategoryFeat.GetSubsByType
{
    public class GetSubsByTypeQuery : IRequest<BaseResponse<IEnumerable<SubCategory>>>
    {
        public string CategoryType { get; set; }

        public GetSubsByTypeQuery(string categoryType)
        {
            CategoryType = categoryType;
        }
    }
}
