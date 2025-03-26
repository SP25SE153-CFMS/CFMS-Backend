using CFMS.Application.Common;
using CFMS.Application.Features.FoodFeat.GetFood;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.ShiftFeat.GetShift
{
    public class GetShiftQueryHandler : IRequestHandler<GetShiftQuery, BaseResponse<Shift>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetShiftQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<Shift>> Handle(GetShiftQuery request, CancellationToken cancellationToken)
        {
            var existShift = _unitOfWork.ShiftRepository.Get(filter: f => f.ShiftId.Equals(request.Id) && f.IsDeleted == false).FirstOrDefault();
            if (existShift == null)
            {
                return BaseResponse<Shift>.FailureResponse(message: "Ca làm không tồn tại");
            }

            return BaseResponse<Shift>.SuccessResponse(data: existShift);
        }
    }
}
