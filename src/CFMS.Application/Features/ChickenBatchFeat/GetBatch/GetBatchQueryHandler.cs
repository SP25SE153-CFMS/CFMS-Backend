using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.GetBatch
{
    public class GetBatchQueryHandler : IRequestHandler<GetBatchQuery, BaseResponse<ChickenBatch>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetBatchQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<BaseResponse<ChickenBatch>> Handle(GetBatchQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
