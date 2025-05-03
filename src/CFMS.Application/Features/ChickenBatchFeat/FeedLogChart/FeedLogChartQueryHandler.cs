using CFMS.Application.Common;
using CFMS.Application.DTOs.FeedLog;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.FeedLogChart
{
    public class FeedLogChartQueryHandler : IRequestHandler<FeedLogChartQuery, BaseResponse<IEnumerable<ChartDataResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public FeedLogChartQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<IEnumerable<ChartDataResponse>>> Handle(FeedLogChartQuery request, CancellationToken cancellationToken)
        {
            var existBatch = _unitOfWork.ChickenBatchRepository.Get(
                filter: cb => cb.ChickenBatchId.Equals(request.BatchId) && !cb.IsDeleted,
                includeProperties: "FeedLogs"
                ).FirstOrDefault();
            if (existBatch == null)
            {
                return BaseResponse<IEnumerable<ChartDataResponse>>.FailureResponse(message: "Lứa không tồn tại");
            }

            var dailyFeedSummary = existBatch.FeedLogs
                .Where(f => f.FeedingDate.HasValue && f.ActualFeedAmount.HasValue)
                .GroupBy(f => f.FeedingDate.Value.Date)
                .Select(g => new ChartDataResponse
                {
                    Date = g.Key,
                    TotalFeed = g.Sum(x => x.ActualFeedAmount.Value)
                })
                .OrderBy(x => x.Date)
                .ToList();
            return BaseResponse<IEnumerable<ChartDataResponse>>.SuccessResponse(data: dailyFeedSummary);
        }
    }
}
