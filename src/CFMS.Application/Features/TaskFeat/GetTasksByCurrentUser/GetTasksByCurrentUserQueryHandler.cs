using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.DTOs.Task;
using CFMS.Application.Features.TaskFeat.GetTasks;
using CFMS.Application.Services.Impl;
using CFMS.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.TaskFeat.GetTasksByCurrentUser
{
    public class GetTasksByCurrentUserQueryHandler : IRequestHandler<GetTasksByCurrentUserQuery, BaseResponse<IEnumerable<TaskResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetTasksByCurrentUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<BaseResponse<IEnumerable<TaskResponse>>> Handle(GetTasksByCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var currentUser = _currentUserService.GetUserId();
            Guid userId = Guid.Parse(_currentUserService.GetUserId());

            var existTasks = _unitOfWork.TaskRepository.GetIncludeMultiLayer(filter: f => (f.FarmId.Equals(request.FarmId) && f.Assignments.Select(x => x.AssignedToId).Contains(userId)) && f.IsDeleted == false,
               include: q => q
                    .Include(t => t.Assignments)
                        .ThenInclude(s => s.AssignedTo)
                            .ThenInclude(s => s.FarmEmployees)
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

            return BaseResponse<IEnumerable<TaskResponse>>.SuccessResponse(data: _mapper.Map<IEnumerable<TaskResponse>>(existTasks));
        }
    }
}
