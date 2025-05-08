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
        public GetSubByCategoryIdQuery(Guid categoryId, Guid farmId)
        {
            CategoryId = categoryId;
            FarmId = farmId;
        }

        public Guid CategoryId { get; set; }
        public Guid FarmId { get; set; }
    }
}
