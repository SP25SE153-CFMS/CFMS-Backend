using AutoMapper;
using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.CategoryFeat.GetSubsByTypeAndFarm
{
    public class GetSubsByTypeAndFarmQueryHandler : IRequestHandler<GetSubsByTypeAndFarmQuery, BaseResponse<IEnumerable<SubCategory>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetSubsByTypeAndFarmQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<IEnumerable<SubCategory>>> Handle(GetSubsByTypeAndFarmQuery request, CancellationToken cancellationToken)
        {
            var subs = _unitOfWork.SubCategoryRepository.Get(filter: c => c.FarmId.Equals(request.FarmId) && c.Category.CategoryType.Equals(request.CategoryType) && c.IsDeleted == false, includeProperties: [x => x.Category]).ToList();
            return BaseResponse<IEnumerable<SubCategory>>.SuccessResponse(data: subs);
        }
    }
}
