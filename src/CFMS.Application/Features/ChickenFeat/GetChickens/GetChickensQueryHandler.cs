using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.ChickenFeat.GetChickens
{
    public class GetChickensQueryHandler : IRequestHandler<GetChickensQuery, BaseResponse<IEnumerable<Chicken>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetChickensQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<IEnumerable<Chicken>>> Handle(GetChickensQuery request, CancellationToken cancellationToken)
        {
            var chickens = _unitOfWork.ChickenRepository.Get(filter: c => c.IsDeleted == false && c.ChickenBatchId.Equals(request.ChickenBatchId));
            return BaseResponse<IEnumerable<Chicken>>.SuccessResponse(data: chickens);
        }
    }
}
