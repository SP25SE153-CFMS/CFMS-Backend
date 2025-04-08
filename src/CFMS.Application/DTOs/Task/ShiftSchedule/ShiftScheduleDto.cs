using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.DTOs.Task.ShiftSchedule
{
    public class ShiftScheduleDto
    {
        public string? ShiftName { get; set; }
        public DateOnly? WorkTime { get; set; }
    }
}
