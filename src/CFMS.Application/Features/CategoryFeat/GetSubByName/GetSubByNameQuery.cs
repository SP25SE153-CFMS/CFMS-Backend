using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.CategoryFeat.GetSubByName
{
    public class GetSubByNameQuery : IRequest<BaseResponse<SubCategory>>
    {
        public GetSubByNameQuery(string subCategoryName)
        {
            SubCategoryName = subCategoryName;
        }

        public string SubCategoryName { get; set; }
    }
}
