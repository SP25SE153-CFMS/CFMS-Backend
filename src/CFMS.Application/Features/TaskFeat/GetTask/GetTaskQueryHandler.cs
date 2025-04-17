using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CFMS.Application.Features.TaskFeat.GetTask
{
    public class GetTaskQueryHandler : IRequestHandler<GetTaskQuery, BaseResponse<Domain.Entities.Task>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTaskQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<Domain.Entities.Task>> Handle(GetTaskQuery request, CancellationToken cancellationToken)
        {
            var existTask = _unitOfWork.TaskRepository.GetIncludeMultiLayer(filter: t => t.TaskId.Equals(request.TaskId) && t.IsDeleted == false,
                include: q => q
                    .Include(t => t.Assignments)
                        .ThenInclude(s => s.AssignedTo)
                    .Include(t => t.TaskType)
                    .Include(t => t.ShiftSchedules)
                        .ThenInclude(s => s.Shift)
                    .Include(t => t.TaskHarvests)
                    .Include(t => t.TaskResources)
                        .ThenInclude(s => s.Resource)
                            .ThenInclude(r => r.Food)
                    .Include(t => t.TaskResources)
                        .ThenInclude(s => s.Resource)
                            .ThenInclude(r => r.Medicine)
                    .Include(t => t.TaskResources)
                        .ThenInclude(s => s.Resource)
                            .ThenInclude(r => r.Equipment)
                    .Include(t => t.TaskResources)
                        .ThenInclude(s => s.ResourceType)
                    .Include(t => t.TaskResources)
                        .ThenInclude(s => s.Resource)
                            .ThenInclude(s => s.Unit)
                    .Include(t => t.TaskResources)
                        .ThenInclude(s => s.Resource)
                            .ThenInclude(s => s.Package)
                    .Include(t => t.TaskResources)
                        .ThenInclude(s => s.Unit)
                    .Include(t => t.TaskLocations)
                        .ThenInclude(s => s.Location)
                    .Include(t => t.TaskLocations)
                        .ThenInclude(s => s.LocationNavigation)
                ).FirstOrDefault();
            if (existTask == null)
            {
                return BaseResponse<Domain.Entities.Task>.FailureResponse(message: "Công việc không tồn tại");
            }

            return BaseResponse<Domain.Entities.Task>.SuccessResponse(data: existTask);
        }
    }
}
