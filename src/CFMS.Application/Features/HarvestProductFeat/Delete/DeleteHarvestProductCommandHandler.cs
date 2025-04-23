using CFMS.Application.Common;
using CFMS.Application.Features.FoodFeat.Delete;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.HarvestProductFeat.Delete
{
    public class DeleteHarvestProductCommandHandler : IRequestHandler<DeleteHarvestProductCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteHarvestProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteHarvestProductCommand request, CancellationToken cancellationToken)
        {
            var existHavest = _unitOfWork.HarvestProductRepository.Get(filter: f => f.HarvestProductId.Equals(request.Id) && f.IsDeleted == false).FirstOrDefault();
            if (existHavest == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Thực phẩm không tồn tại");
            }

            try
            {
                _unitOfWork.HarvestProductRepository.Delete(existHavest);
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
