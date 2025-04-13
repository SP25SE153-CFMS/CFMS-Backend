using CFMS.Application.Common;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.DeleteVaccineLog
{
    public class DeleteVaccineLogCommand : IRequest<BaseResponse<bool>>
    {
        public DeleteVaccineLogCommand(Guid batchId, Guid vaccineLogId)
        {
            BatchId = batchId;
            VaccineLogId = vaccineLogId;
        }

        public Guid BatchId { get; set; }

        public Guid VaccineLogId { get; set; }
    }
}
