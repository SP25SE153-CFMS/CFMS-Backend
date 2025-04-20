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
    public class GetShiftByFarmIdQueryHandler : IRequestHandler<GetShiftByFarmIdQuery, BaseResponse<Shift>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetShiftByFarmIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<Shift>> Handle(GetShiftByFarmIdQuery request, CancellationToken cancellationToken)
        {
            var existShift = _unitOfWork.ShiftRepository.Get(filter: f => f.FarmId.Equals(request.FarmId) && f.IsDeleted == false).FirstOrDefault();
            if (existShift == null)
            {
                return BaseResponse<Shift>.FailureResponse(message: "Ca làm không tồn tại");
            }

            return BaseResponse<Shift>.SuccessResponse(data: existShift);
        }
    }
}
