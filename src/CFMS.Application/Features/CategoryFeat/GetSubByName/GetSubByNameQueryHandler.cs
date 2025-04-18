using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.Features.CategoryFeat.GetSub;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.CategoryFeat.GetSubByName
{
    public class GetSubByNameQueryHandler : IRequestHandler<GetSubByNameQuery, BaseResponse<SubCategory>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetSubByNameQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<SubCategory>> Handle(GetSubByNameQuery request, CancellationToken cancellationToken)
        {
            var existSubCategory = _unitOfWork.SubCategoryRepository.Get(filter: c => c.SubCategoryName.Equals(request.SubCategoryName) && c.IsDeleted == false).FirstOrDefault();
            if (existSubCategory == null)
            {
                return BaseResponse<SubCategory>.FailureResponse(message: "SubCategory không tồn tại");
            }
            return BaseResponse<SubCategory>.SuccessResponse(data: existSubCategory);
        }
    }
}
