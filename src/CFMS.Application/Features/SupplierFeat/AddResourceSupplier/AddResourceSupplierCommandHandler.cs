using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.Features.SupplierFeat.Create;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.SupplierFeat.AddResourceSupplier
{
    public class AddResourceSupplierCommandHandler : IRequestHandler<AddResourceSupplierCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddResourceSupplierCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<bool>> Handle(AddResourceSupplierCommand request, CancellationToken cancellationToken)
        {
            var existResource = _unitOfWork.ResourceRepository.Get(filter: r => r.ResourceId.Equals(request.ResourceId) && r.IsDeleted == false).FirstOrDefault();
            if (existResource == null)
            {
                return BaseResponse<bool>.FailureResponse("Hàng hoá không tồn tại");
            }

            var existSupplier = _unitOfWork.SupplierRepository.Get(filter: s => s.SupplierId.Equals(request.SupplierId) && s.IsDeleted == false).FirstOrDefault();
            if (existSupplier == null)
            {
                return BaseResponse<bool>.FailureResponse("Nhà cung cấp không tồn tại");
            }

            var resourceSupplier = _mapper.Map<ResourceSupplier>(request);
            _unitOfWork.ResourceSupplierRepository.Insert(resourceSupplier);
            var result = await _unitOfWork.SaveChangesAsync();

            return result > 0
                ? BaseResponse<bool>.SuccessResponse("Thêm hàng hoá cho nhà cung cấp thành công")
                : BaseResponse<bool>.FailureResponse("Thêm thất bại");
        }
    }
}
