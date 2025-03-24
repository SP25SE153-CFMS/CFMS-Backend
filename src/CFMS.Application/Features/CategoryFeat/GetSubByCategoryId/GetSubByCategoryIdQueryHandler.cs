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

namespace CFMS.Application.Features.CategoryFeat.GetSubByCategoryId
{
    public class GetSubByCategoryIdQueryHandler : IRequestHandler<GetSubByCategoryIdQuery, BaseResponse<IEnumerable<SubCategory>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetSubByCategoryIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponse<IEnumerable<SubCategory>>> Handle(GetSubByCategoryIdQuery request, CancellationToken cancellationToken)
        {
            var subCategories = _unitOfWork.SubCategoryRepository.Get(filter: c => c.CategoryId.Equals(request.CategoryId) && c.IsDeleted == false).ToList();
            if (subCategories == null)
            {
                return BaseResponse<IEnumerable<SubCategory>>.FailureResponse(message: "Không có danh mục con nào tồn tại" +
                    "");
            }
            return BaseResponse<IEnumerable<SubCategory>>.SuccessResponse(data: subCategories);
        }

    }
}
