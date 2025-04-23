using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.Features.SupplierFeat.AddResourceSupplier;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.SupplierFeat.UpdateResourceSupplier
{
    public class UpdateResourceSupplierCommandHandler : IRequestHandler<UpdateResourceSupplierCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateResourceSupplierCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<bool>> Handle(UpdateResourceSupplierCommand request, CancellationToken cancellationToken)
        {
            var existResourceSupplier = _unitOfWork.ResourceSupplierRepository.Get(filter: r => r.ResourceSupplierId.Equals(request.ResourceSupplierId) && r.IsDeleted == false).FirstOrDefault();
            if (existResourceSupplier == null)
            {
                return BaseResponse<bool>.SuccessResponse("Nhà cung cấp không có loại hàng hoá này");
            }

            var existResource = _unitOfWork.ResourceRepository.Get(filter: r => r.ResourceId.Equals(request.ResourceId) && r.IsDeleted == false).FirstOrDefault();
            if (existResource == null)
            {
                return BaseResponse<bool>.SuccessResponse("Hàng hoá không tồn tại");
            }

            existResourceSupplier.ResourceId = request.ResourceId;
            existResourceSupplier.Description = request.Description;
            existResourceSupplier.Price = request.Price;

            _unitOfWork.ResourceSupplierRepository.Update(existResourceSupplier);
            var result = await _unitOfWork.SaveChangesAsync();

            return result > 0
                ? BaseResponse<bool>.SuccessResponse("Thêm hàng hoá cho nhà cung cấp thành công")
                : BaseResponse<bool>.SuccessResponse("Thêm thất bại");
        }
    }
}
