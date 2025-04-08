using CFMS.Application.Common;
using CFMS.Application.DTOs.Dashboard;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.DashboardFeat.GetDashboard
{
    public class GetDashboardQueryHandler : IRequestHandler<GetDashboardQuery, BaseResponse<DashboardResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetDashboardQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<DashboardResponse>> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
        {
            var existFarm = _unitOfWork.FarmRepository.Get(filter: f => f.FarmId.Equals(request.FarmId) && f.IsDeleted == false, includeProperties: "BreedingAreas,BreedingAreas.ChickenCoops.ChickenBatches.ChickenDetails").FirstOrDefault();
            if (existFarm == null)
            {
                return BaseResponse<DashboardResponse>.FailureResponse(message: "Farm không tồn tại");
            }



            return BaseResponse<DashboardResponse>.SuccessResponse(message: "");
        }
    }
}
