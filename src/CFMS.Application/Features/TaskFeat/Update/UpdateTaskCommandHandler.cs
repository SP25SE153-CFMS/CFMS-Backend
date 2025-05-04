using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.Services.SignalR;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace CFMS.Application.Features.TaskFeat.Update
{
    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly NotiHub _hubContext;

        public UpdateTaskCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, NotiHub hubContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hubContext = hubContext;
        }

        public async Task<BaseResponse<bool>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingTask = _unitOfWork.TaskRepository
                    .GetIncludeMultiLayer(filter: t => t.TaskId == request.TaskId && !t.IsDeleted,
                    include: x => x
                    .Include(i => i.Assignments)
                    .Include(i => i.TaskResources)
                    .Include(i => i.ShiftSchedules)
                    .Include(i => i.TaskLocations)
                    ).FirstOrDefault();

                if (existingTask == null)
                    return BaseResponse<bool>.FailureResponse("Công việc không tồn tại");

                var taskType = _unitOfWork.SubCategoryRepository
                    .Get(filter: t => t.SubCategoryId == request.TaskTypeId && !t.IsDeleted)
                    .FirstOrDefault();

                existingTask.TaskName = request.TaskName?.Trim() ?? existingTask.TaskName;
                existingTask.TaskTypeId = request.TaskTypeId ?? existingTask.TaskTypeId;
                existingTask.Description = request.Description?.Trim() ?? existingTask.Description;
                existingTask.IsHarvest = taskType?.ToString().ToLower() == "harvest" ? 1 : 0;

                if (request.ShiftId != null)
                {
                    existingTask.ShiftSchedules.Clear();

                    var existShift = _unitOfWork.ShiftRepository
                        .Get(filter: s => s.ShiftId == request.ShiftId && !s.IsDeleted)
                        .FirstOrDefault();

                    if (existShift == null)
                        return BaseResponse<bool>.FailureResponse("Ca làm việc không tồn tại");

                    existingTask.ShiftSchedules.Add(new ShiftSchedule
                    {
                        ShiftId = request.ShiftId,
                        Date = DateOnly.FromDateTime(DateTime.Now.ToLocalTime())
                    });
                }

                if (request.TaskResources != null)
                {
                    existingTask.TaskResources.Clear();

                    foreach (var res in request.TaskResources)
                    {
                        var existResource = _unitOfWork.ResourceRepository
                            .Get(filter: r => r.ResourceId == res.ResourceId && !r.IsDeleted)
                            .FirstOrDefault();

                        if (existResource == null)
                            return BaseResponse<bool>.FailureResponse("Vật phẩm không tồn tại");

                        existingTask.TaskResources.Add(new TaskResource
                        {
                            ResourceId = existResource.ResourceId,
                            ResourceTypeId = existResource.ResourceTypeId,
                            UnitId = existResource.UnitId,
                            Quantity = res.SuppliedQuantity
                        });
                    }
                }

                if (request?.LocationId != null)
                {
                    existingTask.TaskLocations.Clear();
                    existingTask.TaskLocations.Add(new TaskLocation
                    {
                        CoopId = request.LocationType == "COOP" ? request.LocationId : null,
                        WareId = request.LocationType == "WARE" ? request.LocationId : null,
                        LocationType = request.LocationType
                    });
                }

                if (request.StartWorkDate != null)
                {
                    existingTask.StartWorkDate = request.StartWorkDate;
                }

                if (request.AssignedTos != null)
                {
                    existingTask.Assignments.Clear();

                    var chosenLeader = request.AssignedTos.Any(x => x.Status == 1);

                    var isHaveLeader = existingTask.Assignments.Any(x => x.Status == 1);

                    if (!chosenLeader && !isHaveLeader)
                    {
                        return BaseResponse<bool>.FailureResponse(message: "Công việc này chưa có đội trưởng đảm nhận");
                    }

                    if (chosenLeader && isHaveLeader)
                    {
                        return BaseResponse<bool>.FailureResponse(message: "Công việc này đã có đội trưởng đảm nhận");
                    }

                    var existUserAssigned = existingTask.Assignments.Any(x => request.AssignedTos.Select(t => t.AssignedToId).Contains(x.AssignedToId ?? Guid.Empty));
                    if (existUserAssigned)
                    {
                        return BaseResponse<bool>.FailureResponse(message: "Người dùng đã được giao công việc này");
                    }

                    foreach (var assignedTo in request.AssignedTos)
                    {
                        var existEmployee = _unitOfWork.UserRepository.Get(filter: u => u.UserId.Equals(assignedTo.AssignedToId) && u.FarmEmployees.Any(fe => fe.FarmId.Equals(existingTask.FarmId)) && u.Status == 1, includeProperties: "FarmEmployees").FirstOrDefault();
                        if (existEmployee == null)
                        {
                            return BaseResponse<bool>.FailureResponse(message: "Người dùng không tồn tại");
                        }

                        var assignment = new Assignment
                        {
                            TaskId = request.TaskId,
                            AssignedDate = request.AssignedDate,
                            AssignedToId = assignedTo.AssignedToId,
                            Note = request.Note,
                            Status = assignedTo.Status,
                        };

                        var noti = new Notification
                        {
                            UserId = assignedTo.AssignedToId,
                            NotificationName = "Thông báo giao việc",
                            NotificationType = "ASSIGNMENT_TASK",
                            Content = "Công việc " + existingTask.TaskName + " đã được giao đến bạn, vui lòng hoàn thành đúng thời hạn",
                            IsRead = 0
                        };

                        existingTask.Status = 1;
                        _unitOfWork.TaskRepository.Update(existingTask);

                        await _hubContext.SendMessage(noti);

                        _unitOfWork.NotificationRepository.Insert(noti);
                        _unitOfWork.AssignmentRepository.Insert(assignment);
                    }
                }

                _unitOfWork.TaskRepository.Update(existingTask);
                var result = await _unitOfWork.SaveChangesAsync();

                return result > 0
                    ? BaseResponse<bool>.SuccessResponse("Cập nhật thành công")
                    : BaseResponse<bool>.SuccessResponse("Cập nhật thất bại");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(ex.Message);
            }
        }
    }
}
