using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.DeleteQuantityLog
{
    public class DeleteQuantityLogCommandHandler : IRequestHandler<DeleteQuantityLogCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteQuantityLogCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteQuantityLogCommand request, CancellationToken cancellationToken)
        {
            var existBatch = _unitOfWork.ChickenBatchRepository.Get(filter: b => b.ChickenBatchId.Equals(request.BatchId) && b.IsDeleted == false).FirstOrDefault();
            if (existBatch == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Lứa nuôi không tồn tại");
            }

            var existQuantityLog = _unitOfWork.QuantityLogRepository.Get(filter: ql => ql.QuantityLogId.Equals(request.QuantityLogId) && ql.IsDeleted == false).FirstOrDefault();
            if (existQuantityLog == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Log không tồn tại");
            }

            try
            {
                existBatch.QuantityLogs.Remove(existQuantityLog);

                _unitOfWork.ChickenBatchRepository.Update(existBatch);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Thêm thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Thêm không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra");
            }
        }
    }
}
