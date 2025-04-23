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

namespace CFMS.Application.Features.MedicineFeat.GetMedicine
{
    public class GetMedicineQueryHandler : IRequestHandler<GetMedicineQuery, BaseResponse<Medicine>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetMedicineQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<Medicine>> Handle(GetMedicineQuery request, CancellationToken cancellationToken)
        {
            var existMedicine = _unitOfWork.MedicineRepository.Get(filter: f => f.MedicineId.Equals(request.Id) && f.IsDeleted == false).FirstOrDefault();
            if (existMedicine == null)
            {
                return BaseResponse<Medicine>.SuccessResponse(message: "Dược phẩm không tồn tại");
            }

            return BaseResponse<Medicine>.SuccessResponse(data: existMedicine);
        }
    }
}
