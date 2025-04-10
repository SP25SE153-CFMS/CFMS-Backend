using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.DeleteVaccineLog
{
    public class DeleteVaccineLogCommand : IRequest<BaseResponse<bool>>
    {
    }
}
