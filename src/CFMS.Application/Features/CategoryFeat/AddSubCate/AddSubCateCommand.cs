using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.CategoryFeat.AddSubCate
{
    public class AddSubCateCommand : IRequest<BaseResponse<bool>>
    {
        public AddSubCateCommand(string? subCategoryName, string? description, string? status, string? dataType, Guid? categoryId)
        {
            SubCategoryName = subCategoryName;
            Description = description;
            Status = status;
            DataType = dataType;
            CategoryId = categoryId;
        }

        public string? SubCategoryName { get; set; }

        public string? Description { get; set; }

        public string? Status { get; set; }

        public string? DataType { get; set; }

        public Guid? CategoryId { get; set; }
    }
}
