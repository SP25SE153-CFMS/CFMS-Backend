using AutoMapper;
using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.SupplierFeat.Create
{
    public class CreateSupplierCommandHandler : IRequestHandler<CreateSupplierCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateSupplierCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<bool>> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            var existSupplier = _unitOfWork.SupplierRepository.Get(filter: s => s.SupplierCode.Equals(request.SupplierCode) || s.SupplierName.Equals(request.SupplierName)).FirstOrDefault();
            if (existSupplier != null)
            {
                return BaseResponse<bool>.FailureResponse("Tên hoặc mã nhà cung cấp đã tồn tại");
            }

            var supplier = _mapper.Map<Supplier>(request);
            _unitOfWork.SupplierRepository.Insert(supplier);
            var result = await _unitOfWork.SaveChangesAsync();

            return result > 0
                ? BaseResponse<bool>.SuccessResponse("Thêm nhà cung cấp thành công")
                : BaseResponse<bool>.FailureResponse("Thêm thất bại");
        }
    }
}
