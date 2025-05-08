using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.CategoryFeat.DeleteSubCategory
{
    public class DeleteSubCategoryCommand : IRequest<BaseResponse<bool>>
    {
        public DeleteSubCategoryCommand(Guid subCategoryId)
        {
            SubCategoryId = subCategoryId;
        }

        public Guid SubCategoryId { get; set; }
    }
}
