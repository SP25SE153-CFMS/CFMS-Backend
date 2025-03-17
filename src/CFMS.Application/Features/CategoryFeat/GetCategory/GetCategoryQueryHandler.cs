using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.DTOs.Category;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.CategoryFeat.GetCategory
{
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, BaseResponse<CategoryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCategoryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<CategoryResponse>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var existCategory = _unitOfWork.CategoryRepository.Get(filter: c => c.CategoryId.Equals(request.CategoryId) && c.IsDeleted == false).FirstOrDefault();
            if (existCategory == null)
            {
                return BaseResponse<CategoryResponse>.FailureResponse(message: "Category không tồn tại");
            }
            return BaseResponse<CategoryResponse>.SuccessResponse(data: _mapper.Map<CategoryResponse>(existCategory));
        }
    }
}
