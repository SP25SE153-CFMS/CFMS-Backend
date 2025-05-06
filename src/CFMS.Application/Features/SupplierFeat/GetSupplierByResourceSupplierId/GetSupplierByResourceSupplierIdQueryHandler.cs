using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.DTOs.Supplier;
using CFMS.Application.Features.SupplierFeat.GetSupplierByResourceId;
using CFMS.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.SupplierFeat.GetSupplierByResourceSupplierId
{
    public class GetSupplierByResourceSupplierIdQueryHandler : IRequestHandler<GetSupplierByResourceSupplierIdQuery, BaseResponse<SupplierResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetSupplierByResourceSupplierIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<SupplierResponse>> Handle(GetSupplierByResourceSupplierIdQuery request, CancellationToken cancellationToken)
        {
            var existResource = _unitOfWork.ResourceSupplierRepository.GetIncludeMultiLayer(x => x.ResourceSupplierId.Equals(request.ResourceSupplierId) && !x.IsDeleted,
                include: x => x
                .Include(t => t.Supplier)
                ).FirstOrDefault();

            if (existResource == null)
            {
                return BaseResponse<SupplierResponse>.FailureResponse("Nhà cung cấp không tồn tại");
            }

            var result = new SupplierResponse
            {
                SupplierId = existResource.SupplierId,
                SupplierName = existResource?.Supplier?.SupplierName,
                SupplierCode = existResource?.Supplier?.SupplierCode,
                ResourceSupplierId = existResource?.ResourceSupplierId
            };

            return BaseResponse<SupplierResponse>.SuccessResponse(result);
        }
    }
}
