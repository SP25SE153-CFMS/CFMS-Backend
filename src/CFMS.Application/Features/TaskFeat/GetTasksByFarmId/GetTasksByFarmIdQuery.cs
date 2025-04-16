using CFMS.Application.Common;
using CFMS.Application.DTOs.Task;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.TaskFeat.GetTaskByFarmId
{
    public class GetTasksByFarmIdQuery : IRequest<BaseResponse<IEnumerable<TaskResponse>>>
    {
        public GetTasksByFarmIdQuery(Guid farmId)
        {
            FarmId = farmId;
        }

        public Guid FarmId { get; set; }
    }
}
