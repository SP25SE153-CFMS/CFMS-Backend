using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.Features.CategoryFeat.GetCategories;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.CategoryFeat.GetSubsByCode
{
    public class GetSubsByCodeQueryHandler : IRequestHandler<GetSubsByTypeQuery, BaseResponse<IEnumerable<SubCategory>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetSubsByCodeQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<IEnumerable<SubCategory>>> Handle(GetSubsByTypeQuery request, CancellationToken cancellationToken)
        {
            var subs = _unitOfWork.SubCategoryRepository.Get(filter: c => c.Category.CategoryType.Equals(request.CategoryType) && c.IsDeleted == false, includeProperties:[x => x.Category]).ToList();
            return BaseResponse<IEnumerable<SubCategory>>.SuccessResponse(data: subs);
        }
    }
}
