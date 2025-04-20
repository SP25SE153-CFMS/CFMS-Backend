using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.CategoryFeat.GetChickenTypes
{
    public class GetChickenTypesQueryHandler : IRequestHandler<GetChickenTypesQuery, BaseResponse<IEnumerable<SubCategory>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetChickenTypesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<IEnumerable<SubCategory>>> Handle(GetChickenTypesQuery request, CancellationToken cancellationToken)
        {
            var subCategories = _unitOfWork.SubCategoryRepository.Get(filter: s => (s.FarmId == null || s.FarmId.Equals(request.FarmId)) && s.Category.CategoryType.Equals("CHICKEN") && s.IsDeleted == false, includeProperties: [s => s.Category, s => s.Chickens]);
            return BaseResponse<IEnumerable<SubCategory>>.SuccessResponse(data: subCategories);
        }
    }
}
