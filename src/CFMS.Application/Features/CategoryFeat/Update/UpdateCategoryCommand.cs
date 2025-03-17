using CFMS.Application.Common;
using CFMS.Domain.Enums.Status;
using CFMS.Domain.Enums.Types;
using MediatR;

namespace CFMS.Application.Features.CategoryFeat.Update
{
    public class UpdateCategoryCommand : IRequest<BaseResponse<bool>>
    {
        public UpdateCategoryCommand(CategoryType? categoryType, string? categoryCode, string? description, CategoryStatus? status, Guid categoryId)
        {
            CategoryType = categoryType;
            CategoryCode = categoryCode;
            Description = description;
            Status = status;
            CategoryId = categoryId;
        }

        public Guid CategoryId { get; set; }

        public CategoryType? CategoryType { get; set; }

        public string? CategoryCode { get; set; }

        public string? Description { get; set; }

        public CategoryStatus? Status { get; set; }
    }
}
