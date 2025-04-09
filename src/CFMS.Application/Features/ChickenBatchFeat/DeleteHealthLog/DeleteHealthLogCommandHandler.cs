using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.DeleteHealthLog
{
    public class DeleteHealthLogCommandHandler : IRequestHandler<DeleteHealthLogCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteHealthLogCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteHealthLogCommand request, CancellationToken cancellationToken)
        {
            var existBatch = _unitOfWork.ChickenBatchRepository.Get(filter: b => b.ChickenBatchId.Equals(request.BatchId) && b.IsDeleted == false, includeProperties: "HealthLogs").FirstOrDefault();
            if (existBatch == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Lứa không tồn tại");
            }

            var existLog = _unitOfWork.HealthLogRepository.Get(filter: hl => hl.HealthLogId.Equals(request.HealthLogId) && hl.IsDeleted == false).FirstOrDefault();
            if (existLog == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Log không tồn tại");
            }


            try
            {
                var temp = existBatch.HealthLogs.ToList();
                temp.RemoveAll(hl => hl.HealthLogId.Equals(existLog.HealthLogId));
                existBatch.HealthLogs = temp;

                _unitOfWork.ChickenBatchRepository.Update(existBatch);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Cập nhật thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Cập nhật không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra");
            }
        }
    }
}
