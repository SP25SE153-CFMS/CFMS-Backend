using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.DTOs.Farm;
using CFMS.Application.DTOs.Task;
using CFMS.Application.Features.TaskFeat.GetTaskByFarmId;
using CFMS.Application.Services.Impl;
using CFMS.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.TaskFeat.GetTasksByStatus
{
    public class GetTasksByStatusQueryHandler : IRequestHandler<GetTasksByStatusQuery, BaseResponse<IEnumerable<TaskDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetTasksByStatusQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<BaseResponse<IEnumerable<TaskDto>>> Handle(GetTasksByStatusQuery request, CancellationToken cancellationToken)
        {
            var currentUser = _currentUserService.GetUserId();
            Guid userId = Guid.Parse(_currentUserService.GetUserId());

            var existFarm = _unitOfWork.FarmRepository.Get(
                filter: f => f.FarmId.Equals(request.FarmId) && !f.IsDeleted
            ).FirstOrDefault();

            if (existFarm == null)
            {
                return BaseResponse<IEnumerable<TaskDto>>.SuccessResponse(message: "Trang trại không tồn tại");
            }

            var tasks = _unitOfWork.TaskRepository.GetIncludeMultiLayer(
                filter: t => t.FarmId.Equals(existFarm.FarmId) && t.Assignments.Any(x => x.AssignedToId.Equals(userId)) && t.Status.Equals(request.Status) && !t.IsDeleted,
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
            );

            var taskDtos = _mapper.Map<IEnumerable<TaskDto>>(tasks);

            return BaseResponse<IEnumerable<TaskDto>>.SuccessResponse(taskDtos);
        }
    }
}
