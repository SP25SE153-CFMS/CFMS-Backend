using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.DTOs.Task;
using CFMS.Application.Features.TaskFeat.GetTasks;
using CFMS.Application.Services.Impl;
using CFMS.Domain.Interfaces;
using MediatR;
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

            var existTasks = _unitOfWork.TaskRepository.Get(filter: f => (f.FarmId.ToString().Equals(request.FarmId) || f.Assignments.Select(x => x.AssignedToId).Contains(userId)) && f.IsDeleted == false,
                  includeProperties: [
                    x => x.Assignments,                      
                    x => x.TaskType,
                    x => x.ShiftSchedules,
                    x => x.TaskHarvests,
                    x => x.TaskResources,
                    x => x.TaskLocations
                      ]);

            return BaseResponse<IEnumerable<TaskResponse>>.SuccessResponse(data: _mapper.Map<IEnumerable<TaskResponse>>(existTasks));
        }
    }
}
