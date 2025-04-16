using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.DTOs.Task;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.TaskFeat.GetTasks
{
    public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, BaseResponse<IEnumerable<TaskResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTasksQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<IEnumerable<TaskResponse>>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
        {
            var tasks = _unitOfWork.TaskRepository.Get(filter: t => t.IsDeleted == false, includeProperties: [t => t.TaskType, t => t.Assignments, t => t.TaskLocations, t => t.ShiftSchedules, t => t.TaskResources]);
            return BaseResponse<IEnumerable<TaskResponse>>.SuccessResponse(data: _mapper.Map<IEnumerable<TaskResponse>>(tasks));
        }
    }
}
