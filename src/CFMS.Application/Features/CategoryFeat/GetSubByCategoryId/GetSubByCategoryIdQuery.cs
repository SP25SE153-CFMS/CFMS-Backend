using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.CategoryFeat.GetSubByCategoryId
{
    public class GetSubByCategoryIdQuery : IRequest<BaseResponse<IEnumerable<SubCategory>>>
    {
        public GetSubByCategoryIdQuery(Guid categoryId)
        {
            CategoryId = categoryId;
        }

        public Guid CategoryId { get; set; }
    }
}
