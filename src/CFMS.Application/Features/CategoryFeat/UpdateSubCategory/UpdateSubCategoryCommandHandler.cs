using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.CategoryFeat.UpdateSubCategory
{
    public class UpdateSubCategoryCommandHandler : IRequestHandler<UpdateSubCategoryCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSubCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(UpdateSubCategoryCommand request, CancellationToken cancellationToken)
        {
            var existSub = _unitOfWork.SubCategoryRepository.Get(filter: s => !s.IsDeleted && s.SubCategoryId.Equals(request.SubCategoryId)).FirstOrDefault();
            if (existSub == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Danh mục không tồn tại");
            }

            try
            {
                existSub.SubCategoryName = request.SubCategoryName;
                existSub.Status = request.Status;
                existSub.DataType = request.DataType;
                existSub.Description = request.Description;

                _unitOfWork.SubCategoryRepository.Update(existSub);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ?
                    BaseResponse<bool>.SuccessResponse(message: "Cập nhật thành công") :
                    BaseResponse<bool>.FailureResponse(message: "Cập nhật không  thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra: " + ex.Message);
            }
        }
    }
}
