using CFMS.Application.Common;
using CFMS.Application.DTOs.ChickenBatch;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.FileSystemGlobbing;

namespace CFMS.Application.Features.ChickenBatchFeat.DashboardChickenBatch
{
    public class DashboardChickenBatchQueryHandler : IRequestHandler<DashboardChickenBatchQuery, BaseResponse<DashboardChickenBatchResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DashboardChickenBatchQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<DashboardChickenBatchResponse>> Handle(DashboardChickenBatchQuery request, CancellationToken cancellationToken)
        {
            var existBatch = _unitOfWork.ChickenBatchRepository.Get(
                filter: b => b.ChickenBatchId.Equals(request.BatchId) && !b.IsDeleted,
                includeProperties: "ChickenDetails,QuantityLogs"
                    ).FirstOrDefault();
            if (existBatch == null)
            {
                return BaseResponse<DashboardChickenBatchResponse>.FailureResponse(message: "Lứa không tồn tại");
            }

            var totalChicken = existBatch.ChickenDetails.Sum(cd => cd.Quantity);
            var deathChicken = existBatch.QuantityLogs.Sum(cd => cd.Quantity);
            var aliveChicken = totalChicken - deathChicken;

            var dashboardChickenBatch = new DashboardChickenBatchResponse
            {
                AliveChicken = aliveChicken.Value,
                DeathChicken = deathChicken.Value,
                TotalChicken = totalChicken.Value,
            };

            return BaseResponse<DashboardChickenBatchResponse>.SuccessResponse(data: dashboardChickenBatch);
        }
    }
}
