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


namespace CFMS.Application.Features.EquipmentFeat.GetEquipment
{
    public class GetEquipmentQueryHandler : IRequestHandler<GetEquipmentQuery, BaseResponse<Equipment>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetEquipmentQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<Equipment>> Handle(GetEquipmentQuery request, CancellationToken cancellationToken)
        {
            var existEquipment = _unitOfWork.EquipmentRepository.Get(filter: f => f.EquipmentId.Equals(request.Id) && f.IsDeleted == false).FirstOrDefault();
            if (existEquipment == null)
            {
                return BaseResponse<Equipment>.SuccessResponse(message: "Trang thiết bị không tồn tại");
            }

            return BaseResponse<Equipment>.SuccessResponse(data: existEquipment);
        }
    }
}
