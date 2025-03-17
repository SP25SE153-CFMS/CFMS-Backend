using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.DTOs.Category;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.CategoryFeat.GetCategories
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, BaseResponse<IEnumerable<CategoryResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCategoriesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<IEnumerable<CategoryResponse>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = _unitOfWork.CategoryRepository.Get(filter: c => c.IsDeleted == false).Select(c => _mapper.Map<CategoryResponse>(c));
            return BaseResponse<IEnumerable<CategoryResponse>>.SuccessResponse(data: categories);
        }
    }
}
