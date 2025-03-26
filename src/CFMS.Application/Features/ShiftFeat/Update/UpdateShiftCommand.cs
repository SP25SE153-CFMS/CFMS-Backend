using CFMS.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.ShiftFeat.Update
{
    public class UpdateShiftCommand : IRequest<BaseResponse<bool>>
    {
        public UpdateShiftCommand(Guid shiftId, string? shiftName, DateTime? startTime, DateTime? endTime)
        {
            ShiftId = shiftId;
            ShiftName = shiftName;
            StartTime = startTime;
            EndTime = endTime;
        }

        public Guid ShiftId { get; set; }

        public string? ShiftName { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }
    }
}
