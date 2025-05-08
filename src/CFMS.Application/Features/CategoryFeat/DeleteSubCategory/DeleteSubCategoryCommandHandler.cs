using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.CategoryFeat.DeleteSubCategory
{
    public class DeleteSubCategoryCommandHandler : IRequestHandler<DeleteSubCategoryCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSubCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteSubCategoryCommand request, CancellationToken cancellationToken)
        {
            var existSub = _unitOfWork.SubCategoryRepository.Get(filter: s => !s.IsDeleted && s.SubCategoryId.Equals(request.SubCategoryId)).FirstOrDefault();
            if (existSub == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Danh mục không tồn tại");
            }

            try
            {
                _unitOfWork.SubCategoryRepository.Delete(existSub);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ?
                    BaseResponse<bool>.SuccessResponse(message: "Xóa thành công") :
                    BaseResponse<bool>.FailureResponse(message: "Xóa thành ko công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra: " + ex.Message);
            }
        }
    }
}
