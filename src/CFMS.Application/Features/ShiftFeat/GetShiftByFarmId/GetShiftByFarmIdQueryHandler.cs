using CFMS.Application.Common;
using CFMS.Application.Features.ShiftFeat.GetShift;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.ShiftFeat.GetShiftByFarmId
{
    public class GetShiftByFarmIdQueryHandler : IRequestHandler<GetShiftByFarmIdQuery, BaseResponse<IEnumerable<Shift>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetShiftByFarmIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<IEnumerable<Shift>>> Handle(GetShiftByFarmIdQuery request, CancellationToken cancellationToken)
        {
            var shifts = _unitOfWork.ShiftRepository.Get(filter: f => (f.FarmId == null || f.FarmId.Equals(request.FarmId)) && f.IsDeleted == false);
            return BaseResponse<IEnumerable<Shift>>.SuccessResponse(data: shifts);
        }
    }
}
