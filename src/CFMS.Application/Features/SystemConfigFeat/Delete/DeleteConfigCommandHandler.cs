using CFMS.Application.Common;
using CFMS.Application.Features.FoodFeat.Delete;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.SystemConfigFeat.Delete
{
    public class DeleteConfigCommandHandler : IRequestHandler<DeleteConfigCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteConfigCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteConfigCommand request, CancellationToken cancellationToken)
        {
            var existConfig = _unitOfWork.SystemConfigRepository.Get(filter: f => f.SystemConfigId.Equals(request.Id) && f.IsDeleted == false).FirstOrDefault();
            if (existConfig == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Cấu hình hệ thống không tồn tại");
            }

            try
            {
                _unitOfWork.SystemConfigRepository.Delete(existConfig);
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
