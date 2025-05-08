using CFMS.Application.Common;
using CFMS.Application.DTOs.FarmEmployee;
using CFMS.Domain.Entities;
using MediatR;

namespace CFMS.Application.Features.FarmFeat.GetFarmEmployees
{
    public class GetFarmEmployeesQuery : IRequest<BaseResponse<IEnumerable<FarmEmployeeResponse>>>
    {
        public GetFarmEmployeesQuery(Guid farmId)
        {
            FarmId = farmId;
        }

        public Guid FarmId { get; set; }
    }
}
