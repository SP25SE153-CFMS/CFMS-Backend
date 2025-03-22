using CFMS.Application.Common;
using CFMS.Domain.Enums.Status;
using CFMS.Domain.Enums.Types;
using MediatR;

namespace CFMS.Application.Features.CategoryFeat.Create
{
    public class CreateCategoryCommand : IRequest<BaseResponse<bool>>
    {
        public CreateCategoryCommand(string? categoryType, string? categoryName, string? description, int? status)
        {
            CategoryType = categoryType;
            CategoryName = categoryName;
            Description = description;
            Status = status;
        }

        public string? CategoryType { get; set; }

        public string? CategoryName { get; set; }

        public string? Description { get; set; }

        public int? Status { get; set; }
    }
}
