using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.DTOs.Task;
using CFMS.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            var tasks = _unitOfWork.TaskRepository.GetIncludeMultiLayer(filter: t => t.FarmId.Equals(request.FarmId) && t.IsDeleted == false,
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
                    .Include(t => t.TaskResources)
                        .ThenInclude(s => s.Resource)
                            .ThenInclude(z => z.Unit)
                    .Include(t => t.TaskResources)
                        .ThenInclude(s => s.Resource)
                            .ThenInclude(z => z.Package)
                    .Include(t => t.FeedLogs)
                        .ThenInclude(s => s.Resource)
                            .ThenInclude(z => z.Food)
                    .Include(t => t.FeedLogs)
                        .ThenInclude(s => s.Resource)
                            .ThenInclude(z => z.Equipment)
                    .Include(t => t.FeedLogs)
                        .ThenInclude(s => s.Resource)
                            .ThenInclude(z => z.Medicine)
                    .Include(t => t.FeedLogs)
                        .ThenInclude(s => s.Resource)
                            .ThenInclude(z => z.Unit)
                    .Include(t => t.FeedLogs)
                        .ThenInclude(s => s.Resource)
                            .ThenInclude(z => z.Package)
                    ).ToList();

            return BaseResponse<IEnumerable<TaskResponse>>.SuccessResponse(data: _mapper.Map<IEnumerable<TaskResponse>>(tasks));
        }
    }
}
