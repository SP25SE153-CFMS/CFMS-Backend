using AutoMapper;
using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.TaskFeat.Create
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public CreateTaskCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<bool>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                foreach (var date in request.StartWorkDate)
                {
                    var task = _mapper.Map<Domain.Entities.Task>(request);

                    task.StartWorkDate = date;

                    foreach (var shiftId in request.ShiftIds)
                    {
                        var existShitf = _unitOfWork.ShiftRepository.Get(noTracking: true, filter: s => s.ShiftId.Equals(shiftId) && s.IsDeleted == false).FirstOrDefault();
                        if (existShitf == null)
                        {
                            return BaseResponse<bool>.FailureResponse(message: "Ca làm việc không tồn tại");
                        }

                        task.ShiftSchedules.Add(new ShiftSchedule
                        {
                            ShiftId = existShitf.ShiftId,
                            Date = DateOnly.FromDateTime(DateTime.Now.ToLocalTime()),
                        });
                    }

                    foreach (var taskResource in request.TaskResources)
                    {
                        var existResource = _unitOfWork.ResourceRepository.Get(filter: r => r.ResourceId.Equals(taskResource.ResourceId) && r.IsDeleted == false).FirstOrDefault();
                        if (existResource == null)
                        {
                            return BaseResponse<bool>.FailureResponse(message: "Resource không tồn tại");
                        }

                        task.TaskResources.Add(new TaskResource
                        {
                            ResourceId = existResource.ResourceId,
                            ResourceTypeId = existResource.ResourceTypeId,
                            UnitId = existResource.UnitId,
                            Quantity = taskResource.Quantity,
                        });
                    }

                    task.TaskLocations.Add(new TaskLocation
                    {
                        //CoopId = request.LocationId,
                        CoopId = request.LocationType.Equals("COOP") ? request.LocationId : null,
                        WareId = request.LocationType.Equals("WARE") ? request.LocationId : null,
                        LocationType = request.LocationType,
                    });

                    _unitOfWork.TaskRepository.Insert(task);
                }

                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Tạo thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Tạo không thành công");
            }
            catch (Exception e)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra");
            }
        }
    }
}
