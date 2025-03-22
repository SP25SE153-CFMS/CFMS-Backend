using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.AddGrowthStage
{
    public class AddGrowthStageCommandHandler : IRequestHandler<AddGrowthStageCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddGrowthStageCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(AddGrowthStageCommand request, CancellationToken cancellationToken)
        {
            var existBatch = _unitOfWork.ChickenBatchRepository.Get(filter: b => b.ChickenBatchId.Equals(request.ChickenBatchId) && b.IsDeleted == false).FirstOrDefault();
            if (existBatch == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Lứa nuôi không tồn tại");
            }

            var existStage = _unitOfWork.GrowthStageRepository.Get(filter: s => s.GrowthStageId.Equals(request.GrowthStageId) && s.IsDeleted == false).FirstOrDefault();
            if (existStage == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Giai đoạn phát triển không tồn tại");
            }

            try
            {
                existBatch.GrowthBatches.Add(new GrowthBatch
                {
                    ChickenBatchId = request.ChickenBatchId,
                    GrowthStageId = request.GrowthStageId,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    AvgWeight = request.AvgWeight,
                    MortalityRate = request.MortalityRate,
                    FeedConsumption = request.FeedConsumption,
                    Note = request.Note,
                    Status = 1
                });

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
