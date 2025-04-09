using AutoMapper;
using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.OpenChickenBatch
{
    public class OpenChickenBatchCommandHandler : IRequestHandler<OpenChickenBatchCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public OpenChickenBatchCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<bool>> Handle(OpenChickenBatchCommand request, CancellationToken cancellationToken)
        {
            var existChicken = _unitOfWork.ChickenRepository.Get(filter: c => c.ChickenId.Equals(request.ChickenId) && c.IsDeleted == false).FirstOrDefault();
            if (existChicken == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Gà không tồn tại");
            }

            var stages = _unitOfWork.GrowthStageRepository.Get(
                filter: s => s.StageCode.Equals(request.StageCode),
                orderBy: s => s.OrderBy(s => s.MinAgeWeek)
                );
            if (!stages.Any())
            {
                return BaseResponse<bool>.FailureResponse(message: "Giai đoạn phát triển không tồn tại");
            }

            try
            {
                var batch = _mapper.Map<ChickenBatch>(request);
                batch.Status = DateOnly.FromDateTime(batch.StartDate.Value) > DateOnly.FromDateTime(DateTime.Now.ToLocalTime()) ? 0 : 1;
                batch.CurrentStageId = stages.FirstOrDefault().GrowthStageId;

                foreach (var chickenDetail in request.ChickenDetailRequests)
                {
                    batch.ChickenDetails.Add(new ChickenDetail
                    {
                        ChickenId = existChicken.ChickenId,
                        Quantity = chickenDetail.Quantity,
                        Gender = chickenDetail.Gender,
                    });
                }

                foreach (var stage in stages)
                {
                    batch.GrowthBatches.Add(new GrowthBatch
                    {
                        GrowthStageId = stage.GrowthStageId,
                        StartDate = batch.StartDate.Value.AddDays(stage.MinAgeWeek.Value * 7),
                        EndDate = batch.StartDate.Value.AddDays(stage.MaxAgeWeek.Value * 7)
                    });
                }

                _unitOfWork.ChickenBatchRepository.Insert(batch);
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
