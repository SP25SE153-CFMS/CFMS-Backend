using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.TaskFeat.CompleteTask
{
    public class CompleteTaskCommandHandler : IRequestHandler<CompleteTaskCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompleteTaskCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(CompleteTaskCommand request, CancellationToken cancellationToken)
        {
            var existTask = _unitOfWork.TaskRepository.Get(filter: t => t.TaskId.Equals(request.TaskId) && t.IsDeleted == false, includeProperties: [t => t.TaskLocations]).FirstOrDefault();
            if (existTask == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Task không tồn tại");
            }

            try
            {
                existTask.Status = 1;

                var location = existTask.TaskLocations.FirstOrDefault();
                if (location.LocationType.Equals("COOP"))
                {
                    var taskLog = new TaskLog
                    {
                        ChickenCoopId = location.CoopId,
                        CompletedAt = DateTime.Now.ToLocalTime(),
                        TaskId = existTask.TaskId,
                        Note = request.Note,
                    };

                    _unitOfWork.TaskLogRepository.Insert(taskLog);
                }

                _unitOfWork.TaskRepository.Update(existTask);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Cập nhật thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Cập nhật không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra");
            }
        }
    }
}
