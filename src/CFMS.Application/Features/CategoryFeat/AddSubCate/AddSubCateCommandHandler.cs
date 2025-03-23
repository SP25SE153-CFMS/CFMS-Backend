using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.CategoryFeat.AddSubCate
{
    public class AddSubCateCommandHandler : IRequestHandler<AddSubCateCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddSubCateCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(AddSubCateCommand request, CancellationToken cancellationToken)
        {
            var existCategory = _unitOfWork.SubCategoryRepository.Get(filter: c => (c.SubCategoryName.Equals(request.SubCategoryName)) && c.IsDeleted == false).FirstOrDefault();
            if (existCategory != null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Tên danh mục đã tồn tại");
            }

            var existCate = _unitOfWork.CategoryRepository.Get(filter: c => c.IsDeleted == false).FirstOrDefault();
            if (existCate == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Loại danh mục không tồn tại");
            }

            try
            {
                existCate.SubCategories.Add(new SubCategory()
                {
                    Description = request.Description,
                    SubCategoryName = request.SubCategoryName,
                    Status = request.Status,
                    DataType = request.DataType,
                    CategoryId = request.CategoryId
                });
                _unitOfWork.CategoryRepository.Update(existCate);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Tạo danh mục thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Tạo  danh mục không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra:" + ex.Message);
            }
        }
    }
}
