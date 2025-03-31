using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.CategoryFeat.Update
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var existCategory = _unitOfWork.CategoryRepository.Get(filter: c => c.CategoryId.Equals(request.CategoryId) && c.IsDeleted == false).FirstOrDefault();
            if (existCategory == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Danh mục không tồn tại");
            }

            if (existCategory.CategoryName.Equals(request.CategoryName) == null && existCategory.CategoryId != request.CategoryId)
            {
                return BaseResponse<bool>.FailureResponse(message: "Mã danh mục đã tồn tại");
            }

            try
            {
                existCategory.CategoryName = request.CategoryName;
                existCategory.CategoryType = request.CategoryType;
                existCategory.Status = (int)request.Status;
                existCategory.Description = request.Description;

                _unitOfWork.CategoryRepository.Update(existCategory);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Cập nhật thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Cập nhật không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra");
            }
        }
    }
}
