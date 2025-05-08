using CFMS.Application.Common;
using CFMS.Domain.Enums.Status;
using CFMS.Domain.Enums.Types;
using MediatR;

namespace CFMS.Application.Features.CategoryFeat.Update
{
    public class UpdateCategoryCommand : IRequest<BaseResponse<bool>>
    {
        public UpdateCategoryCommand(string? categoryType, string? categoryName, string? description, int? status, Guid categoryId)
        {
            CategoryType = categoryType;
            CategoryName = categoryName;
            Description = description;
            Status = status;
            CategoryId = categoryId;
        }

        public Guid CategoryId { get; set; }

        public string? CategoryType { get; set; }

        public string? CategoryName { get; set; }

        public string? Description { get; set; }

        public int? Status { get; set; }
    }
}
