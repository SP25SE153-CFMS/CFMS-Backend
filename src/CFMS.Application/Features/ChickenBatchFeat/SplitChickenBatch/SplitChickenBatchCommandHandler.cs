using AutoMapper;
using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System.Linq;

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
            var existParentBatch = _unitOfWork.ChickenBatchRepository
                .Get(filter: pb => pb.ChickenBatchId == request.ParentBatchId && !pb.IsDeleted, includeProperties: "ChickenDetails")
                .FirstOrDefault();

            if (existParentBatch == null)
                return BaseResponse<bool>.FailureResponse(message: "Lứa không tồn tại");

            if (existParentBatch.ChickenCoopId.Equals(request.ChickenCoopId))
                return BaseResponse<bool>.FailureResponse(message: "Không thể chọn chung chuồng");

            var existCoop = _unitOfWork.ChickenCoopRepository
                .Get(filter: c => c.ChickenCoopId == request.ChickenCoopId && c.Status == 0 && !c.IsDeleted)
                .FirstOrDefault();

            if (existCoop == null)
                return BaseResponse<bool>.FailureResponse(message: "Chuồng gà không tồn tại");

            var stages = _unitOfWork.GrowthStageRepository
                .Get(filter: s => s.StageCode == request.StageCode, orderBy: q => q.OrderBy(s => s.OrderNum))
                .ToList();

            if (stages.Count == 0)
                return BaseResponse<bool>.FailureResponse(message: "Giai đoạn phát triển không tồn tại");

            try
            {
                var systemConfig = _unitOfWork.SystemConfigRepository.Get(filter: c => c.SettingName.Equals("MinQuantityChickenInBatch") && c.Status == 1).FirstOrDefault();
                var totalChickenInBatch = existParentBatch.ChickenDetails.Select(cd => cd.Quantity).Sum();
                var totalChickenSplit = request.ChickenDetailRequests.Select(c => c.Quantity).Sum();
                if (totalChickenInBatch - totalChickenSplit < systemConfig.SettingValue)
                {
                    return BaseResponse<bool>.FailureResponse(message: "Vượt quá số lượng tách đàn");
                }

                var newBatch = _mapper.Map<ChickenBatch>(request);
                var currentDate = DateOnly.FromDateTime(DateTime.Now);
                var startDate = DateOnly.FromDateTime(newBatch.StartDate!.Value);

                newBatch.Status = startDate > currentDate ? 0 : 1;
                newBatch.CurrentStageId = stages.First().GrowthStageId;

                foreach (var chickenDetail in request.ChickenDetailRequests)
                {
                    var matchedDetail = existParentBatch.ChickenDetails
                        .FirstOrDefault(c => c.Gender == chickenDetail.Gender);

                    if (matchedDetail == null || matchedDetail.Quantity < chickenDetail.Quantity)
                        return BaseResponse<bool>.FailureResponse(message: "Gà không tồn tại hoặc đã vượt quá số lượng tách đàn");

                    matchedDetail.Quantity -= chickenDetail.Quantity;

                    if (matchedDetail.Quantity == 0)
                    {
                        var temp = existParentBatch.ChickenDetails.Remove(matchedDetail);
                        //_unitOfWork.ChickenDetailRepository.Delete(matchedDetail);
                    }

                    newBatch.ChickenDetails.Add(new ChickenDetail
                    {
                        ChickenId = existParentBatch.ChickenId,
                        Quantity = chickenDetail.Quantity,
                        Gender = chickenDetail.Gender
                    });
                }

                existParentBatch.QuantityLogs.Add(new QuantityLog
                {
                    LogDate = DateTime.Now,
                    LogType = 1,
                    Notes = request.Notes,
                    Quantity = request.ChickenDetailRequests.Sum(x => x.Quantity)
                });

                foreach (var stage in stages)
                {
                    newBatch.GrowthBatches.Add(new GrowthBatch
                    {
                        GrowthStageId = stage.GrowthStageId,
                        StartDate = newBatch.StartDate.Value.AddDays(stage.MinAgeWeek!.Value * 7),
                        EndDate = newBatch.StartDate.Value.AddDays(stage.MaxAgeWeek!.Value * 7)
                    });
                }

                _unitOfWork.ChickenBatchRepository.Update(existParentBatch);
                _unitOfWork.ChickenBatchRepository.Insert(newBatch);

                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0
                    ? BaseResponse<bool>.SuccessResponse(message: "Tạo thành công")
                    : BaseResponse<bool>.FailureResponse(message: "Tạo không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra: " + ex.Message);
            }
        }
    }
}
