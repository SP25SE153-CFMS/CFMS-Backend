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
                ).FirstOrDefault();

            //var existTask = _unitOfWork.TaskRepository.Get(
            //        filter: t => t.TaskId.Equals(request.TaskId) && t.IsDeleted == false,
            //        includeProperties: "Assignments,Assignments.AssignedTo,TaskType,ShiftSchedules,ShiftSchedules.Shift,TaskResources,TaskResources.ResourceType,TaskResources.Resource,TaskResources.Resource.Food,TaskResources.Resource.Medicine,TaskResources.Resource.Equipment,TaskResources.Resource.Chicken,TaskResources.Resource.HarvestProduct,TaskLocations,TaskLocations.Location,TaskLocations.LocationNavigation,TaskResources.Resource.Unit,TaskResources.Resource.Package,TaskResources.Resource.Unit"
            //    ).FirstOrDefault();
            //    include: q => q)
            if (existTask == null)
            {
                return BaseResponse<TaskResponse>.FailureResponse(message: "Công việc không tồn tại");
            }

            return BaseResponse<TaskResponse>.SuccessResponse(data: _mapper.Map<TaskResponse>(existTask));
        }
    }
}
