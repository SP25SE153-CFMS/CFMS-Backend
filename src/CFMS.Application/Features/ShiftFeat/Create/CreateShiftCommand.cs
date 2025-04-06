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
        public CreateShiftCommand(string? shiftName, TimeOnly? startTime, TimeOnly? endTime)
        {
            ShiftName = shiftName;
            StartTime = startTime;
            EndTime = endTime;
        }

        public string? ShiftName { get; set; }

        public TimeOnly? StartTime { get; set; }

        public TimeOnly? EndTime { get; set; }
    }
}
