using CFMS.Application.Common;
using CFMS.Application.DTOs.Category;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.CategoryFeat.GetCategory
{
    public class GetCategoryQuery : IRequest<BaseResponse<CategoryResponse>>
    {
        public GetCategoryQuery(Guid categoryId)
        {
            CategoryId = categoryId;
        }

        public Guid CategoryId { get; set; }
    }
}
