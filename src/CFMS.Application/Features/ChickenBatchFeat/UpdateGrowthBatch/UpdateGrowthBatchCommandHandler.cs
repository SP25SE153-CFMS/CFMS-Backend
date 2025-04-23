using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.UpdateGrowthBatch
{
    public class UpdateGrowthBatchCommandHandler : IRequestHandler<UpdateGrowthBatchCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateGrowthBatchCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(UpdateGrowthBatchCommand request, CancellationToken cancellationToken)
        {
            var existGrowthBatch = _unitOfWork.GrowthBatchRepository.Get(filter: gb => gb.GrowthBatchId.Equals(request.GrowthBatchId) && gb.IsDeleted == false).FirstOrDefault();
            if (existGrowthBatch == null)
            {
                return BaseResponse<bool>.SuccessResponse(message: "Giai đoạn phát triển không tồn tại");
            }

            try
            {
                //existGrowthBatch.StartDate = request.StartDate;
                //existGrowthBatch.EndDate = request.EndDate;
                //existGrowthBatch.AvgWeight = request.AvgWeight;
                //existGrowthBatch.MortalityRate = request.MortalityRate;
                //existGrowthBatch.Note = request.Note;
                //existGrowthBatch.Status = request.Status;

                _unitOfWork.GrowthBatchRepository.Update(existGrowthBatch);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Cập nhật thành công");
                }
                return BaseResponse<bool>.SuccessResponse(message: "Cập nhật không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra:" + ex.Message);
            }
        }
    }
}
