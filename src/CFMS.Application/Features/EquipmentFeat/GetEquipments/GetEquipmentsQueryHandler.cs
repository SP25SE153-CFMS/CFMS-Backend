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

namespace CFMS.Application.Features.EquipmentFeat.GetEquipments
{
    public class GetEquipmentsQueryHandler : IRequestHandler<GetEquipmentsQuery, BaseResponse<IEnumerable<Equipment>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetEquipmentsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<IEnumerable<Equipment>>> Handle(GetEquipmentsQuery request, CancellationToken cancellationToken)
        {
            var equipments = _unitOfWork.EquipmentRepository.Get(filter: f => f.IsDeleted == false);
            return BaseResponse<IEnumerable<Equipment>>.SuccessResponse(_mapper.Map<IEnumerable<Equipment>>(equipments));
        }
    }
}
