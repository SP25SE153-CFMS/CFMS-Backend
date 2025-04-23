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

namespace CFMS.Application.Features.WarehouseFeat.GetWare
{
    public class GetWareQueryHandler : IRequestHandler<GetWareQuery, BaseResponse<Warehouse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetWareQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<Warehouse>> Handle(GetWareQuery request, CancellationToken cancellationToken)
        {
            var existWare = _unitOfWork.WarehouseRepository.Get(filter: f => f.WareId.Equals(request.Id) && f.IsDeleted == false).FirstOrDefault();
            if (existWare == null)
            {
                return BaseResponse<Warehouse>.SuccessResponse(message: "Kho không tồn tại");
            }

            return BaseResponse<Warehouse>.SuccessResponse(data: existWare);
        }
    }
}
