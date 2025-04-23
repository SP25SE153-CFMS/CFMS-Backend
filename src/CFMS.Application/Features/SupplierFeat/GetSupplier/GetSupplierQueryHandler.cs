using CFMS.Application.Common;
using CFMS.Application.Features.FarmFeat.GetFarm;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.SupplierFeat.GetSupplier
{
    public class GetSupplierQueryHandler : IRequestHandler<GetSupplierQuery, BaseResponse<Supplier>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSupplierQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<Supplier>> Handle(GetSupplierQuery request, CancellationToken cancellationToken)
        {
            var existSupplier = _unitOfWork.SupplierRepository.Get(filter: f => f.SupplierId.Equals(request.Id) && f.IsDeleted == false, includeProperties: [s => s.ResourceSuppliers]).FirstOrDefault();
            if (existSupplier == null)
            {
                return BaseResponse<Supplier>.SuccessResponse(message: "Nhà cung cấp không tồn tại");
            }
            return BaseResponse<Supplier>.SuccessResponse(data: existSupplier);
        }
    }
}
