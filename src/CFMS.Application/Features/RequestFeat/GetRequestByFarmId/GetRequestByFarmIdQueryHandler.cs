using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.RequestFeat.GetRequestByFarmId
{
    public class GetRequestByFarmIdQueryHandler : IRequestHandler<GetRequestByFarmIdQuery, BaseResponse<IEnumerable<Request>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetRequestByFarmIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<IEnumerable<Request>>> Handle(GetRequestByFarmIdQuery request, CancellationToken cancellationToken)
        {
            var existFarm = _unitOfWork.FarmRepository.Get(filter: f => f.FarmId.Equals(request.FarmId) && !f.IsDeleted).FirstOrDefault();
            if (existFarm == null)
            {
                return BaseResponse<IEnumerable<Request>>.FailureResponse(message: "Farm không tồn tại");
            }

            var requests = _unitOfWork.RequestRepository.Get(filter: r => r.CreatedByUser.FarmEmployees.Any(u => u.FarmId.Equals(request.FarmId)) && !r.IsDeleted);
            return BaseResponse<IEnumerable<Request>>.SuccessResponse(data: requests);
        }
    }
}
