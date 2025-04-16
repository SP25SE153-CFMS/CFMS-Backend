using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.DTOs.Task;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CFMS.Application.Features.TaskFeat.GetTask
{
    public class GetTaskQueryHandler : IRequestHandler<GetTaskQuery, BaseResponse<TaskResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTaskQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<TaskResponse>> Handle(GetTaskQuery request, CancellationToken cancellationToken)
        {
            var existTask = _unitOfWork.TaskRepository.GetIncludeMultiLayer(filter: t => t.TaskId.Equals(request.TaskId) && t.IsDeleted == false,
                include: q => q
                    .Include(t => t.Assignments)
                        .ThenInclude(s => s.AssignedTo)
                    .Include(t => t.TaskType)
                    .Include(t => t.ShiftSchedules)
                        .ThenInclude(s => s.Shift)
                    .Include(t => t.TaskLocations)
                        .ThenInclude(s => s.Location)
                    .Include(t => t.TaskLocations)
                        .ThenInclude(s => s.LocationNavigation)
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
                ).FirstOrDefault();
            if (existTask == null)
            {
                return BaseResponse<TaskResponse>.FailureResponse(message: "Công việc không tồn tại");
            }

            return BaseResponse<TaskResponse>.SuccessResponse(data: _mapper.Map<TaskResponse>(existTask));
        }
    }
}
