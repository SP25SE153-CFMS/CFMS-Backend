using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System.Linq.Expressions;
using Twilio.TwiML.Voice;

namespace CFMS.Application.Features.ChickenBatchFeat.GetBatch
{
    public class GetBatchQueryHandler : IRequestHandler<GetBatchQuery, BaseResponse<ChickenBatch>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetBatchQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<ChickenBatch>> Handle(GetBatchQuery request, CancellationToken cancellationToken)
        {
            var existBatch = _unitOfWork.ChickenBatchRepository.Get(
            filter: b => b.ChickenBatchId.Equals(request.Id) && !b.IsDeleted,
            includeProperties: "Chicken,Chicken.ChickenDetails,GrowthBatches,GrowthBatches.GrowthStage,FeedLogs,HealthLogs,QuantityLogs,VaccineLogs"
            ).FirstOrDefault();

            if (existBatch == null)
            {
                return BaseResponse<ChickenBatch>.FailureResponse(message: "Lứa không tồn tại");
            }
            return BaseResponse<ChickenBatch>.SuccessResponse(data: existBatch);

        }
    }
}
