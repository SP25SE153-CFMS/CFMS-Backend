using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.AddVaccineLog
{
    public class AddVaccineLogCommand : IRequest<BaseResponse<bool>>
    {
    }
}
