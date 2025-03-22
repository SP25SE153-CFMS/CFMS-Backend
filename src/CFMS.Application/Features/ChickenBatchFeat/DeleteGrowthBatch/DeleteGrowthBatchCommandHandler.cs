using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.DeleteGrowthBatch
{
    public class DeleteGrowthBatchCommandHandler : IRequestHandler<DeleteGrowthBatchCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteGrowthBatchCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteGrowthBatchCommand request, CancellationToken cancellationToken)
        {
            var existBatch = _unitOfWork.ChickenBatchRepository.Get(filter: b => b.ChickenBatchId.Equals(request.ChickenBatchId) && b.IsDeleted == false).FirstOrDefault();
            if (existBatch == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Lứa không tồn tại");
            }

            var existGrowthBatch = _unitOfWork.GrowthBatchRepository.Get(filter: gb => gb.GrowthBatchId.Equals(request.GrowthBatchId) && gb.IsDeleted == false).FirstOrDefault();
            if (existGrowthBatch == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "GrowthBatch không tồn tại");
            }

            try
            {
                existBatch.GrowthBatches.Remove(existGrowthBatch);

                _unitOfWork.ChickenBatchRepository.Update(existBatch);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Xóa thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Xóa không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra");
            }
        }
    }
}
