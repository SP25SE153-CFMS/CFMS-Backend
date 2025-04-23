using CFMS.Application.Common;
using CFMS.Application.Features.SupplierFeat.Create;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.SupplierFeat.Delete
{
    public class DeleteSupplierCommandHandler : IRequestHandler<DeleteSupplierCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSupplierCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
        {
            var existSupplier = _unitOfWork.SupplierRepository.Get(filter: f => f.SupplierId.Equals(request.SupplierId) && f.IsDeleted == false).FirstOrDefault();
            if (existSupplier == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Nhà cung cấp không tồn tại");
            }

            try
            {
                _unitOfWork.SupplierRepository.Delete(existSupplier);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Xóa thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Xoá không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra:" + ex.Message);
            }
        }
    }
}
