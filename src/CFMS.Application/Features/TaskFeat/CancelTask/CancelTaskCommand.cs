using CFMS.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.TaskFeat.CancelTask
{
    public class CancelTaskCommand : IRequest<BaseResponse<bool>>
    {
        public Guid? TaskId { get; set; }

        public CancelTaskCommand(Guid? taskId)
        {
            TaskId = taskId;
        }
    }
}
