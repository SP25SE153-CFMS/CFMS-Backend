using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.Features.FoodFeat.GetFoods;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.HarvestProductFeat.GetHarvestProducts
{
    public class GetHarvestProductsQueryHandler : IRequestHandler<GetHarvestProductsQuery, BaseResponse<IEnumerable<HarvestProduct>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetHarvestProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<IEnumerable<HarvestProduct>>> Handle(GetHarvestProductsQuery request, CancellationToken cancellationToken)
        {
            var havests = _unitOfWork.HarvestProductRepository.Get(filter: f => f.IsDeleted == false);
            return BaseResponse<IEnumerable<HarvestProduct>>.SuccessResponse(_mapper.Map<IEnumerable<HarvestProduct>>(havests));
        }
    }
}
