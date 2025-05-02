using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.QuantityLogDetail
{
    public class QuantityLogDetailQueryDetail : IRequestHandler<QuantityLogDetailQuery, BaseResponse<QuantityLog>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public QuantityLogDetailQueryDetail(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<QuantityLog>> Handle(QuantityLogDetailQuery request, CancellationToken cancellationToken)
        {
            var existQuantityLog = _unitOfWork.QuantityLogRepository.Get(
                filter: l => l.QuantityLogId.Equals(request.Id) && !l.IsDeleted,
                includeProperties: "QuantityLogDetails"
                ).FirstOrDefault();
            return BaseResponse<QuantityLog>.SuccessResponse(data: existQuantityLog);
        }
    }
}
