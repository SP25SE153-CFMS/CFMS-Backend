using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.CategoryFeat.Delete
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var existCategory = _unitOfWork.CategoryRepository.Get(filter: c => c.CategoryId.Equals(request.CategoryId) && c.IsDeleted == false).FirstOrDefault();
            if (existCategory == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Danh mục không tồn tại");
            }

            try
            {
                _unitOfWork.CategoryRepository.Delete(existCategory);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Xóa thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Xoá không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra");
            }
        }
    }
}
