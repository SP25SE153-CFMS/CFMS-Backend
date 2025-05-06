using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.DTOs.Supplier;
using CFMS.Application.Features.SupplierFeat.GetSuppliers;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.SupplierFeat.GetSupplierByResourceId
{
    public class GetSupplierByResourceIdQueryHandler : IRequestHandler<GetSupplierByResourceIdQuery, BaseResponse<IEnumerable<SupplierResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetSupplierByResourceIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<IEnumerable<SupplierResponse>>> Handle(GetSupplierByResourceIdQuery request, CancellationToken cancellationToken)
        {
            var existResource = _unitOfWork.ResourceRepository.GetIncludeMultiLayer(x => x.ResourceId.Equals(request.ResourceId) && !x.IsDeleted,
                include: x => x
                .Include(t => t.ResourceSuppliers)
                    .ThenInclude(t => t.Supplier)
                ).FirstOrDefault();

            if (existResource == null)
            {
                return BaseResponse<IEnumerable<SupplierResponse>>.FailureResponse("Hàng hoá không tồn tại");
            }

            var result = existResource.ResourceSuppliers.Select(t =>
            {
                return new SupplierResponse
                {
                    SupplierId = t.SupplierId,
                    SupplierName = t?.Supplier?.SupplierName,
                    SupplierCode = t?.Supplier?.SupplierCode,
                    ResourceSupplierId = t.ResourceSupplierId
                };
            })
            .Where(x => x != null)
            .ToList();
            return BaseResponse<IEnumerable<SupplierResponse>>.SuccessResponse(result);
        }
    }
}
