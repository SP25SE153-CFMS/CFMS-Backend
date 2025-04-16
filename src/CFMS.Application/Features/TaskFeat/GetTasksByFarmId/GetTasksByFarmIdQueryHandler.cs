using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.DTOs.Task;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.TaskFeat.GetTaskByFarmId
{
    public class GetTasksByFarmIdQueryHandler : IRequestHandler<GetTasksByFarmIdQuery, BaseResponse<IEnumerable<TaskResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTasksByFarmIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<IEnumerable<TaskResponse>>> Handle(GetTasksByFarmIdQuery request, CancellationToken cancellationToken)
        {
            var tasks = _unitOfWork.TaskRepository.Get(filter: t => t.FarmId.Equals(request.FarmId) && t.IsDeleted == false,
                includeProperties: [
                      x => x.Assignments,
                      x => x.TaskType,
                    x => x.ShiftSchedules,
                    x => x.TaskHarvests,
                    x => x.TaskResources,
                    x => x.TaskLocations
                      ]);
            return BaseResponse<IEnumerable<TaskResponse>>.SuccessResponse(data: _mapper.Map<IEnumerable<TaskResponse>>(tasks));
        }
    }
}
