using CFMS.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.TaskFeat.GetTasksByCurrentUser
{
    public class GetTasksByCurrentUserQuery : IRequest<BaseResponse<IEnumerable<Domain.Entities.Task>>>
    {
        public GetTasksByCurrentUserQuery(Guid farmId)
        {
            FarmId = farmId;
        }

        public Guid FarmId { get; set; }
    }
}
