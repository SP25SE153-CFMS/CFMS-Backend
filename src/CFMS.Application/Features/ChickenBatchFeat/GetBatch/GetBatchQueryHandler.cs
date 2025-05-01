using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.DTOs.ChickenBatch;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System.Linq.Expressions;

namespace CFMS.Application.Features.ChickenBatchFeat.GetBatch
{
    public class GetBatchQueryHandler : IRequestHandler<GetBatchQuery, BaseResponse<ChickenBatchResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBatchQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<ChickenBatchResponse>> Handle(GetBatchQuery request, CancellationToken cancellationToken)
        {
            var existBatch = _unitOfWork.ChickenBatchRepository.Get(
            filter: b => b.ChickenBatchId.Equals(request.Id) && !b.IsDeleted,
            includeProperties: "Chicken,GrowthBatches,GrowthBatches.GrowthStage,GrowthBatches.GrowthStage.NutritionPlan,FeedLogs,HealthLogs,QuantityLogs,VaccineLogs,ChickenDetails"
            ).FirstOrDefault();

            if (existBatch == null)
            {
                return BaseResponse<ChickenBatchResponse>.SuccessResponse(message: "Lứa không tồn tại");
            }

            var totalChicken = existBatch.ChickenDetails.Sum(cd => cd.Quantity);
            var deathChicken = existBatch.QuantityLogs.Where(l => l.LogType == 0).Sum(cd => cd.Quantity);
            var aliveChicken = totalChicken - deathChicken;

            var batch = _mapper.Map<ChickenBatchResponse>(existBatch);
            batch.AliveChicken = aliveChicken.Value;
            batch.DeathChicken = deathChicken.Value;
            batch.TotalChicken = totalChicken.Value;

            return BaseResponse<ChickenBatchResponse>.SuccessResponse(data: batch);

        }
    }
}
