using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.Features.ResourceFeat.GetResources;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.FoodFeat.GetFoods
{
    public class GetFoodsQueryHandler : IRequestHandler<GetFoodsQuery, BaseResponse<IEnumerable<Food>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetFoodsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<IEnumerable<Food>>> Handle(GetFoodsQuery request, CancellationToken cancellationToken)
        {
            var foods = _unitOfWork.FoodRepository.Get(filter: f => f.IsDeleted == false);
            return BaseResponse<IEnumerable<Food>>.SuccessResponse(_mapper.Map<IEnumerable<Food>>(foods));
        }
    }
}
