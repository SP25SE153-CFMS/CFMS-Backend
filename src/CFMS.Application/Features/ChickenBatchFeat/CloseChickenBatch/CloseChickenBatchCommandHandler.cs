﻿using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.CloseChickenBatch
{
    public class CloseChickenBatchCommandHandler : IRequestHandler<CloseChickenBatchCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CloseChickenBatchCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(CloseChickenBatchCommand request, CancellationToken cancellationToken)
        {
            var existBatch = _unitOfWork.ChickenBatchRepository.Get(filter: b => b.ChickenBatchId.Equals(request.ChickenBatchId) && b.IsDeleted == false, includeProperties: "ChickenCoop").FirstOrDefault();
            if (existBatch == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Lứa không tồn tại");
            }

            var existCoop = _unitOfWork.ChickenCoopRepository.Get(filter: c => c.ChickenCoopId.Equals(existBatch.ChickenCoopId) && !c.IsDeleted).FirstOrDefault();
            if (existCoop == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Chuồng không tồn tại");
            }

            try
            {
                existBatch.EndDate = DateTime.UtcNow.ToLocalTime().AddHours(7);
                existBatch.Status = 2;

                existCoop.Status = 0;

                _unitOfWork.ChickenCoopRepository.Update(existCoop);
                _unitOfWork.ChickenBatchRepository.Update(existBatch);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result >= 2)
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
