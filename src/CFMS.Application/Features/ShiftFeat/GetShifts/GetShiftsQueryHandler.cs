using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.Features.FoodFeat.GetFoods;
using CFMS.Application.Features.ShiftFeat.GetShift;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.ShiftFeat.GetShifts
{
    public class GetShiftsQueryHandler : IRequestHandler<GetShiftsQuery, BaseResponse<IEnumerable<Shift>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetShiftsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<IEnumerable<Shift>>> Handle(GetShiftsQuery request, CancellationToken cancellationToken)
        {
            var shifts = _unitOfWork.ShiftRepository.Get(filter: f => f.IsDeleted == false);
            return BaseResponse<IEnumerable<Shift>>.SuccessResponse(_mapper.Map<IEnumerable<Shift>>(shifts));
        }
    }
}
