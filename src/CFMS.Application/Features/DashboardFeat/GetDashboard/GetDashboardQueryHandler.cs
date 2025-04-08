using CFMS.Application.Common;
using CFMS.Application.DTOs.Dashboard;
using MediatR;

namespace CFMS.Application.Features.DashboardFeat.GetDashboard
{
    public class GetDashboardQueryHandler : IRequestHandler<GetDashboardQuery, BaseResponse<DashboardResponse>>
    {
        public Task<BaseResponse<DashboardResponse>> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
