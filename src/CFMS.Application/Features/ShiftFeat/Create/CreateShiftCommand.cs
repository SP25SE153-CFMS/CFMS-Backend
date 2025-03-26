using CFMS.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.ShiftFeat.Create
{
    public class CreateShiftCommand : IRequest<BaseResponse<bool>>
    {
        public CreateShiftCommand(string? shiftName, DateTime? startTime, DateTime? endTime)
        {
            ShiftName = shiftName;
            StartTime = startTime;
            EndTime = endTime;
        }

        public string? ShiftName { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }
    }
}
