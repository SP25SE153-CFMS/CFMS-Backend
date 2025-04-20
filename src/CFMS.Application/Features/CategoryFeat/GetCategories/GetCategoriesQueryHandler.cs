using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.DTOs.Category;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.CategoryFeat.GetCategories
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, BaseResponse<IEnumerable<Category>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCategoriesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<IEnumerable<Category>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = _unitOfWork.CategoryRepository.Get(filter: c => c.IsDeleted == false, includeProperties: "SubCategories").ToList();
            return BaseResponse<IEnumerable<Category>>.SuccessResponse(data: categories);
        }
    }
}
