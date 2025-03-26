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

namespace CFMS.Application.Features.SystemConfigFeat.GetConfigs
{
    public class GetConfigsQueryHandler : IRequestHandler<GetConfigsQuery, BaseResponse<IEnumerable<SystemConfig>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetConfigsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<IEnumerable<SystemConfig>>> Handle(GetConfigsQuery request, CancellationToken cancellationToken)
        {
            var configs = _unitOfWork.SystemConfigRepository.Get(filter: f => f.IsDeleted == false);
            return BaseResponse<IEnumerable<SystemConfig>>.SuccessResponse(_mapper.Map<IEnumerable<SystemConfig>>(configs));
        }
    }
}
