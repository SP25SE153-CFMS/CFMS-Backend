using AutoMapper;
using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.TaskFeat.Update
{
    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateTaskCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<bool>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingTask = _unitOfWork.TaskRepository
                    .Get(filter: t => t.FarmId == request.FarmId && !t.IsDeleted, includeProperties: "TaskResources,ShiftSchedules,TaskLocations")
                    .FirstOrDefault();

                if (existingTask == null)
                    return BaseResponse<bool>.FailureResponse("Công việc không tồn tại");

                var taskType = _unitOfWork.SubCategoryRepository
                    .Get(filter: t => t.SubCategoryId == request.TaskTypeId && !t.IsDeleted)
                    .FirstOrDefault();

                existingTask.TaskName = request.TaskName?.Trim();
                existingTask.TaskTypeId = request.TaskTypeId;
                existingTask.Description = request.Description?.Trim();
                existingTask.IsHarvest = taskType?.ToString().ToLower() == "harvest" ? 1 : 0;

                existingTask.ShiftSchedules.Clear();
                if (request.ShiftIds != null)
                {
                    foreach (var shiftId in request.ShiftIds)
                    {
                        var existShift = _unitOfWork.ShiftRepository
                            .Get(filter: s => s.ShiftId == shiftId && !s.IsDeleted)
                            .FirstOrDefault();

                        if (existShift == null)
                            return BaseResponse<bool>.FailureResponse("Ca làm việc không tồn tại");

                        existingTask.ShiftSchedules.Add(new ShiftSchedule
                        {
                            ShiftId = shiftId,
                            Date = DateOnly.FromDateTime(DateTime.Now)
                        });
                    }
                }

                existingTask.TaskResources.Clear();
                if (request.TaskResources != null)
                {
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

                existingTask.TaskLocations.Clear();
                existingTask.TaskLocations.Add(new TaskLocation
                {
                    CoopId = request.LocationType == "COOP" ? request.LocationId : null,
                    WareId = request.LocationType == "WARE" ? request.LocationId : null,
                    LocationType = request.LocationType
                });

                if (request.StartWorkDate != null && request.StartWorkDate.Length > 0)
                {
                    existingTask.StartWorkDate = request.StartWorkDate.First();
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
