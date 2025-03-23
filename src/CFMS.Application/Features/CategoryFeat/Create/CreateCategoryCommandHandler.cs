using AutoMapper;
using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.CategoryFeat.Create
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existCategory = _unitOfWork.CategoryRepository.Get(filter: c => (c.CategoryName.Equals(request.CategoryName) || c.CategoryType.Equals(request.CategoryType)) && c.IsDeleted == false).FirstOrDefault();
                if (existCategory != null)
                {
                    return BaseResponse<bool>.FailureResponse(message: "Name hoặc Type đã tồn tại");
                }

                _unitOfWork.CategoryRepository.Insert(_mapper.Map<Category>(request));
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Tạo Category thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Tạo Category không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra");
            }
        }
    }
}
