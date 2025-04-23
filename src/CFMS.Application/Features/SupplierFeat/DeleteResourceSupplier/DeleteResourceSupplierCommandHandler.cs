using CFMS.Application.Common;
using CFMS.Application.Features.SupplierFeat.Delete;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.SupplierFeat.DeleteResourceSupplier
{
    public class DeleteResourceSupplierCommandHandler : IRequestHandler<DeleteResourceSupplierCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteResourceSupplierCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteResourceSupplierCommand request, CancellationToken cancellationToken)
        {
            var existResourceSupplier = _unitOfWork.ResourceSupplierRepository.Get(filter: f => f.ResourceSupplierId.Equals(request.ResourceSupplierId) && f.IsDeleted == false).FirstOrDefault();
            if (existResourceSupplier == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Nhà cung cấp không có loại hàng hoá này");
            }

            try
            {
                _unitOfWork.SupplierRepository.Delete(existResourceSupplier);
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
