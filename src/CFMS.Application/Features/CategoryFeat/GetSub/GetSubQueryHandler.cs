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

namespace CFMS.Application.Features.CategoryFeat.GetSub
{
    public class GetSubQueryHandler : IRequestHandler<GetSubQuery, BaseResponse<SubCategory>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetSubQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<SubCategory>> Handle(GetSubQuery request, CancellationToken cancellationToken)
        {
            var existSubCategory = _unitOfWork.SubCategoryRepository.Get(filter: c => c.SubCategoryId.Equals(request.SubCategoryId) && c.IsDeleted == false).FirstOrDefault();
            if (existSubCategory == null)
            {
                return BaseResponse<SubCategory>.SuccessResponse(message: "Loại danh mục con không tồn tại");
            }
            return BaseResponse<SubCategory>.SuccessResponse(data: existSubCategory);
        }
    }
}
