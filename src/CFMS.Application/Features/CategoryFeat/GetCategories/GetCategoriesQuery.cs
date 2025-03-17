using CFMS.Application.Common;
using CFMS.Application.DTOs.Category;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.CategoryFeat.GetCategories
{
    public class GetCategoriesQuery : IRequest<BaseResponse<IEnumerable<CategoryResponse>>>
    {
    }
}
