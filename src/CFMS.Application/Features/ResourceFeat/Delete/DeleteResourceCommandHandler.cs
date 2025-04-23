using CFMS.Application.Common;
using CFMS.Application.Features.FoodFeat.Delete;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.ResourceFeat.Delete
{
    public class DeleteResourceCommandHandler : IRequestHandler<DeleteResourceCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteResourceCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteResourceCommand request, CancellationToken cancellationToken)
        {
            var existResource = _unitOfWork.ResourceRepository.Get(filter: f => f.ResourceId.Equals(request.Id) && f.IsDeleted == false).FirstOrDefault();
            if (existResource == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Hàng hoá không tồn tại");
            }

            try
            {
                _unitOfWork.ResourceRepository.Delete(existResource);
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
