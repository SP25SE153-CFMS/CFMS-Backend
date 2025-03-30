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
                var task = _mapper.Map<Domain.Entities.Task>(request);
                task.FrequencySchedules.Add(new FrequencySchedule
                {
                    TimeUnitId = request.TimeUnitId,
                    StartWorkDate = request.StartWorkDate,
                    EndWorkDate = request.EndWorkDate,
                    Frequency = request.Frequency,
                });
                task.ShiftSchedules.Add(new ShiftSchedule
                {
                    ShiftId = request.ShiftId,
                    Date = request.Date,
                });
                task.TaskLocations.Add(new TaskLocation
                {
                    CoopId = request.LocationId,
                    //CoopId = request.LocationType == 1 ? request.LocationId : null,
                    //WareId = request.LocationType == 0 ? request.LocationId : null,
                    LocationType = request.LocationType,
                });

                _unitOfWork.TaskRepository.Insert(task);
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
