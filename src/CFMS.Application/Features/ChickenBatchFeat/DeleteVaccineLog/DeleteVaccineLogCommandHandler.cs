using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.DeleteVaccineLog
{
    public class DeleteVaccineLogCommandHandler : IRequestHandler<DeleteVaccineLogCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteVaccineLogCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteVaccineLogCommand request, CancellationToken cancellationToken)
        {
            var existBatch = _unitOfWork.ChickenBatchRepository.Get(filter: b => b.ChickenBatchId.Equals(request.BatchId) && b.IsDeleted == false).FirstOrDefault();
            if (existBatch == null)
            {
                return BaseResponse<bool>.SuccessResponse(message: "Lứa nuôi không tồn tại");
            }

            var existVaccineLog = _unitOfWork.VaccineLogRepository.Get(filter: ql => ql.VaccineLogId.Equals(request.VaccineLogId) && ql.IsDeleted == false).FirstOrDefault();
            if (existVaccineLog == null)
            {
                return BaseResponse<bool>.SuccessResponse(message: "Log không tồn tại");
            }

            try
            {
                var temp = existBatch.VaccineLogs.ToList();
                temp.RemoveAll(hl => hl.VaccineLogId.Equals(existVaccineLog.VaccineLogId));
                existBatch.VaccineLogs = temp;

                _unitOfWork.VaccineLogRepository.Delete(existVaccineLog);
                _unitOfWork.ChickenBatchRepository.Update(existBatch);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Thêm thành công");
                }
                return BaseResponse<bool>.SuccessResponse(message: "Thêm không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra");
            }
        }
    }
}
