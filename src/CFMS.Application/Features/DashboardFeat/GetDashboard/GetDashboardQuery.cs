using CFMS.Application.Common;
using CFMS.Application.DTOs.Dashboard;
using MediatR;

namespace CFMS.Application.Features.DashboardFeat.GetDashboard
{
    public class GetDashboardQuery : IRequest<BaseResponse<DashboardResponse>>
    {
    }
}
