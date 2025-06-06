﻿using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.GetBatchs
{
    public class GetBatchsQueryHandler : IRequestHandler<GetBatchsQuery, BaseResponse<IEnumerable<ChickenBatch>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetBatchsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<IEnumerable<ChickenBatch>>> Handle(GetBatchsQuery request, CancellationToken cancellationToken)
        {
            var batchs = _unitOfWork.ChickenBatchRepository.Get(filter: b => b.IsDeleted == false && b.ChickenCoopId.Equals(request.CoopId),
                orderBy: batch => batch.OrderBy(x => x.Status),
                includeProperties: [
                    batch => batch.Chicken,
                    batch => batch.Chicken.ChickenDetails,
                    batch => batch.FeedLogs,
                    batch => batch.HealthLogs,
                    batch => batch.QuantityLogs,
                    batch => batch.VaccineLogs]).ToList();
            return BaseResponse<IEnumerable<ChickenBatch>>.SuccessResponse(data: batchs);
        }
    }
}
