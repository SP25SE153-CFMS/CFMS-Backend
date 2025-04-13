using AutoMapper;
using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.SplitChickenBatch
{
    public class SplitChickenBatchCommandHandler : IRequestHandler<SplitChickenBatchCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SplitChickenBatchCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<bool>> Handle(SplitChickenBatchCommand request, CancellationToken cancellationToken)
        {
            var existParentBatch = _unitOfWork.ChickenBatchRepository.Get(filter: pb => pb.ChickenBatchId.Equals(request.ParentBatchId) && pb.IsDeleted == false, includeProperties: "ChickenDetails").FirstOrDefault();
            if (existParentBatch == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Lứa không tồn tại");
            }

            var existCoop = _unitOfWork.ChickenCoopRepository.Get(filter: c => c.ChickenCoopId.Equals(request.ChickenCoopId) && c.Status == 0 && c.IsDeleted == false).FirstOrDefault();
            if (existCoop == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Chuồng gà không tồn tại");
            }

            var existChicken = _unitOfWork.ChickenRepository.Get(filter: c => c.ChickenId.Equals(request.ChickenId) && c.IsDeleted == false).FirstOrDefault();
            if (existChicken == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Gà không tồn tại");
            }

            var stages = _unitOfWork.GrowthStageRepository.Get(
                filter: s => s.StageCode.Equals(request.StageCode),
                orderBy: s => s.OrderBy(s => s.OrderNum)
                );
            if (!stages.Any())
            {
                return BaseResponse<bool>.FailureResponse(message: "Giai đoạn phát triển không tồn tại");
            }

            try
            {
                var newBatch = _mapper.Map<ChickenBatch>(request);
                newBatch.Status = DateOnly.FromDateTime(newBatch.StartDate.Value) > DateOnly.FromDateTime(DateTime.Now.ToLocalTime()) ? 0 : 1;
                newBatch.CurrentStageId = stages.FirstOrDefault().GrowthStageId;

                var temp = existParentBatch.ChickenDetails;
                foreach (var chickenDetail in request.ChickenDetailRequests)
                {
                    var chickenDetailTemp = temp.FirstOrDefault(c => c.Gender.Value == chickenDetail.Gender);
                    if (chickenDetailTemp == null || chickenDetailTemp.Quantity < chickenDetail.Quantity)
                    {
                        return BaseResponse<bool>.FailureResponse("Gà không tồn tại hoặc đã vượt quá số lượng tách đàn");
                    }
                    chickenDetailTemp.Quantity -= chickenDetail.Quantity;

                    newBatch.ChickenDetails.Add(new ChickenDetail
                    {
                        ChickenId = existChicken.ChickenId,
                        Quantity = chickenDetail.Quantity,
                        Gender = chickenDetail.Gender,
                    });
                }
                existParentBatch.ChickenDetails = temp;
                existParentBatch.QuantityLogs.Add(new QuantityLog
                {
                    LogDate = DateTime.Now.ToLocalTime(),
                    LogType = 1,
                    Notes = request.Notes,
                    Quantity = request.ChickenDetailRequests.Select(x => x.Quantity).Sum()
                });

                foreach (var stage in stages)
                {
                    newBatch.GrowthBatches.Add(new GrowthBatch
                    {
                        GrowthStageId = stage.GrowthStageId,
                        StartDate = newBatch.StartDate.Value.AddDays(stage.MinAgeWeek.Value * 7),
                        EndDate = newBatch.StartDate.Value.AddDays(stage.MaxAgeWeek.Value * 7)
                    });
                }

                _unitOfWork.ChickenBatchRepository.Update(existParentBatch);
                _unitOfWork.ChickenBatchRepository.Insert(newBatch);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Tạo thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Tạo không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra");
            }
        }
    }
}
