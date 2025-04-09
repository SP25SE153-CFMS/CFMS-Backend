using CFMS.Application.Common;
using CFMS.Application.DTOs.Task;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.TaskFeat.GetTasksByStatus
{
    public class GetTasksByStatusQuery : IRequest<BaseResponse<IEnumerable<TaskDto>>>
    {
        public GetTasksByStatusQuery(int status, Guid farmId)
        {
            Status = status;
            FarmId = farmId;
        }

        public int Status { get; set; }

        public Guid FarmId { get; set; }
    }
}
