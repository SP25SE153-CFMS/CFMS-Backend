using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.CategoryFeat.Delete
{
    public class DeleteCategoryCommand : IRequest<BaseResponse<bool>>
    {
        public DeleteCategoryCommand(Guid categoryId)
        {
            CategoryId = categoryId;
        }

        public Guid CategoryId { get; set; }
    }
}
