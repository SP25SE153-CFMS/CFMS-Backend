﻿using CFMS.Application.DTOs.Task.ShiftSchedule;
using CFMS.Application.DTOs.Task.TaskResource;
using CFMS.Application.DTOs.Task.Assignment;
using CFMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CFMS.Application.DTOs.Task.Loggers;

namespace CFMS.Application.DTOs.Task
{
    public class TaskDto
    {
        public Guid TaskId { get; set; }

        public string? TaskName { get; set; }

        public string? TaskType { get; set; }

        public string? Description { get; set; }

        //public int? IsHavest { get; set; }

        public string? TaskLocation { get; set; }
            
        public int? Status { get; set; }

        public List<ShiftScheduleDto> shiftScheduleList { get; set; } = new List<ShiftScheduleDto>();

        public List<TaskResourceDto> resourceList { get; set; } = new List<TaskResourceDto>();

        public List<AssignmentDto> assignmentList { get; set; } = new List<AssignmentDto>();

        public List<FeedLogDto> feedLogList { get; set; } = new List<FeedLogDto>();
    }
}
