using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.CategoryFeat.UpdateSubCategory
{
    public class UpdateSubCategoryCommand : IRequest<BaseResponse<bool>>
    {
        public UpdateSubCategoryCommand(Guid subCategoryId, string? subCategoryName, string? description, int? status, string? dataType)
        {
            SubCategoryId = subCategoryId;
            SubCategoryName = subCategoryName;
            Description = description;
            Status = status;
            DataType = dataType;
        }

        public Guid SubCategoryId { get; set; }

        public string? SubCategoryName { get; set; }

        public string? Description { get; set; }

        public int? Status { get; set; }

        public string? DataType { get; set; }
    }
}
