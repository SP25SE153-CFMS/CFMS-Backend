using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.FarmFeat.GetFarm
{
    public class GetFarmQueryHandler : IRequestHandler<GetFarmQuery, BaseResponse<Farm>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetFarmQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<Farm>> Handle(GetFarmQuery request, CancellationToken cancellationToken)
        {
            var existFarm = _unitOfWork.FarmRepository.Get(filter: f => f.FarmId.Equals(request.Id) && f.IsDeleted == false).FirstOrDefault();
            if (existFarm == null)
            {
                return BaseResponse<Farm>.FailureResponse(message: "Farm không tồn tại");
            }
            return BaseResponse<Farm>.SuccessResponse(data: existFarm);
        }
    }
}
