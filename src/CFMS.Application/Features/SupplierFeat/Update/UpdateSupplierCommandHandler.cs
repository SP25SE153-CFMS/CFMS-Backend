using CFMS.Application.Common;
using CFMS.Application.Features.FarmFeat.Update;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.SupplierFeat.Update
{
    public class UpdateSupplierCommandHanlder : IRequestHandler<UpdateSupplierCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSupplierCommandHanlder(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
            var existSupplier = _unitOfWork.SupplierRepository.Get(filter: s => s.SupplierId.Equals(request.SupplierId) && s.IsDeleted == false).FirstOrDefault();
            if (existSupplier == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Nhà cung cấp không tồn tại");
            }

            var existNameCode = _unitOfWork.SupplierRepository.Get(filter: s => s.SupplierCode.Equals(request.SupplierCode) || s.SupplierName.Equals(request.SupplierName) && s.IsDeleted == false && s.SupplierId != request.SupplierId).FirstOrDefault();
            if (existSupplier != null)
            {
                return BaseResponse<bool>.FailureResponse("Tên hoặc mã nhà cung cấp đã tồn tại");
            }

            try
            {
                existSupplier.SupplierName = request.SupplierName;
                existSupplier.SupplierCode = request.SupplierCode;
                existSupplier.PhoneNumber = request.PhoneNumber;
                existSupplier.BankAccount = request.BankAccount;
                existSupplier.Status = request.Status;
                existSupplier.Address = request.Address;

                _unitOfWork.SupplierRepository.Update(existSupplier);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Cập nhật thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Cập nhật không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra:" + ex.Message);
            }
        }
    }
}
