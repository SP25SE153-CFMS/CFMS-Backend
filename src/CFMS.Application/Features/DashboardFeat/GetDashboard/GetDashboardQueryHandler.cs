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
            var existFarm = _unitOfWork.FarmRepository.Get(filter: f => f.FarmId.Equals(request.FarmId) && f.IsDeleted == false, includeProperties: "BreedingAreas,BreedingAreas.ChickenCoops.ChickenBatches.ChickenDetails,FarmEmployees,FarmEmployees.User").FirstOrDefault();
            if (existFarm == null)
            {
                return BaseResponse<DashboardResponse>.SuccessResponse(message: "Farm không tồn tại");
            }

            var users = _unitOfWork.FarmEmployeeRepository.Get(
                filter:
                    fe => fe.FarmId.Equals(request.FarmId) &&
                    fe.User!.Status == 1,
                includeProperties: "User"
                );

            var batches = _unitOfWork.ChickenBatchRepository.Get(
                filter:
                    cb => cb.Status == 1 &&
                    cb.ChickenCoop!.BreedingArea!.FarmId.Equals(request.FarmId),
                includeProperties: "ChickenDetails"
                    );

            var totalChicken = batches.SelectMany(b => b.ChickenDetails).Sum(cd => cd.Quantity);
            var totalEmployee = users.Select(u => u.User).Count();

            var dashBoardResponse = new DashboardResponse
            {
                ChickenBatches = batches,
                TotalChicken = totalChicken,
                TotalEmployee = totalEmployee
            };


            return BaseResponse<DashboardResponse>.SuccessResponse(data: dashBoardResponse);
        }
    }
}
