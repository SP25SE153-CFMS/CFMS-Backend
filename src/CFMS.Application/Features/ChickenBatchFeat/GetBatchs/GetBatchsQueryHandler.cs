using CFMS.Application.Common;
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

        public Task<BaseResponse<IEnumerable<ChickenBatch>>> Handle(GetBatchsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
