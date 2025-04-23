using CFMS.Application.Common;
using CFMS.Application.Features.FoodFeat.Delete;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.WarehouseFeat.Delete
{
    public class DeleteWareCommandHandler : IRequestHandler<DeleteWareCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteWareCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteWareCommand request, CancellationToken cancellationToken)
        {
            var existWare = _unitOfWork.WarehouseRepository.Get(filter: f => f.WareId.Equals(request.Id) && f.IsDeleted == false).FirstOrDefault();
            if (existWare == null)
            {
                return BaseResponse<bool>.SuccessResponse(message: "Kho không tồn tại");
            }

            try
            {
                _unitOfWork.WarehouseRepository.Delete(existWare);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Xóa thành công");
                }
                return BaseResponse<bool>.SuccessResponse(message: "Xoá không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra:" + ex.Message);
            }
        }
    }
}
