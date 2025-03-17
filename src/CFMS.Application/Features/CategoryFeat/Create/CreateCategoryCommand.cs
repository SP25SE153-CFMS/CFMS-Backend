using CFMS.Application.Common;
using CFMS.Domain.Enums.Status;
using CFMS.Domain.Enums.Types;
using MediatR;

namespace CFMS.Application.Features.CategoryFeat.Create
{
    public class CreateCategoryCommand : IRequest<BaseResponse<bool>>
    {
        public CreateCategoryCommand(CategoryType? categoryType, string? categoryCode, string? description, CategoryStatus? status)
        {
            CategoryType = categoryType;
            CategoryCode = categoryCode;
            Description = description;
            Status = status;
        }

        public CategoryType? CategoryType { get; set; }

        public string? CategoryCode { get; set; }

        public string? Description { get; set; }

        public CategoryStatus? Status { get; set; }
    }
}
