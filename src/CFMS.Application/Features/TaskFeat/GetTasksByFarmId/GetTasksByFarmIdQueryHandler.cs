using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.TaskFeat.GetTaskByFarmId
{
    public class GetTasksByFarmIdQueryHandler : IRequestHandler<GetTasksByFarmIdQuery, BaseResponse<IEnumerable<Domain.Entities.Task>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTasksByFarmIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<IEnumerable<Domain.Entities.Task>>> Handle(GetTasksByFarmIdQuery request, CancellationToken cancellationToken)
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
            return BaseResponse<IEnumerable<Domain.Entities.Task>>.SuccessResponse(data: tasks);
        }
    }
}
