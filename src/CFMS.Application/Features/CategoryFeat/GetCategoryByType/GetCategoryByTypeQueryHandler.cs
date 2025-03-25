using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.Features.CategoryFeat.GetCategory;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.CategoryFeat.GetCategoryByType
{
    public class GetCategoryByTypeQueryHandler : IRequestHandler<GetCategoryByTypeQuery, BaseResponse<Category>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCategoryByTypeQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<Category>> Handle(GetCategoryByTypeQuery request, CancellationToken cancellationToken)
        {
            var existCategory = _unitOfWork.CategoryRepository.Get(filter: c => c.CategoryType.Equals(request.CategoryType) && c.IsDeleted == false, includeProperties: "SubCategories").FirstOrDefault();
            if (existCategory == null)
            {
                return BaseResponse<Category>.FailureResponse(message: "Category không tồn tại");
            }
            return BaseResponse<Category>.SuccessResponse(data: existCategory);
        }
    }
}
