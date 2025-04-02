using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.Features.TaskFeat.GetTasks;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.TaskFeat.GetTaskByFarmId
{
    public class GetTasksByFarmIdQueryHandler : IRequestHandler<GetTasksByFarmIdQuery, BaseResponse<IEnumerable<Domain.Entities.Task>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTasksByFarmIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<IEnumerable<Domain.Entities.Task>>> Handle(GetTasksByFarmIdQuery request, CancellationToken cancellationToken)
        {
            var tasks = _unitOfWork.TaskRepository.Get(filter: t => t.FarmId.Equals(request.FarmId) && t.IsDeleted == false);
            return BaseResponse<IEnumerable<Domain.Entities.Task>>.SuccessResponse(_mapper.Map<IEnumerable<Domain.Entities.Task>>(tasks));
        }
    }
}
