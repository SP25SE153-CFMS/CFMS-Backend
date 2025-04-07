using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.Features.WarehouseFeat.GetWares;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.WarehouseFeat.GetWaresByFarmId
{
    public class GetWaresByFarmIdQueryHandler : IRequestHandler<GetWaresByFarmIdQuery, BaseResponse<IEnumerable<Warehouse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetWaresByFarmIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<IEnumerable<Warehouse>>> Handle(GetWaresByFarmIdQuery request, CancellationToken cancellationToken)
        {
            var wares = _unitOfWork.WarehouseRepository.Get(filter: f => f.FarmId.Equals(request.FarmId) && f.IsDeleted == false);
            return BaseResponse<IEnumerable<Warehouse>>.SuccessResponse(_mapper.Map<IEnumerable<Warehouse>>(wares));
        }
    }
}
