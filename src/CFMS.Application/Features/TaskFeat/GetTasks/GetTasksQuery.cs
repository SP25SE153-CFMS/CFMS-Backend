using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.TaskFeat.GetTasks
{
    public class GetTasksQuery : IRequest<BaseResponse<IEnumerable<Domain.Entities.Task>>>
    {
        public GetTasksQuery() { }
    }
}
