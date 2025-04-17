using AutoMapper;
using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.AssignmentFeat.AssignEmployee
{
    public class AssignEmployeeCommandHandler : IRequestHandler<AssignEmployeeCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public AssignEmployeeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<bool>> Handle(AssignEmployeeCommand request, CancellationToken cancellationToken)
        {
            var task = _unitOfWork.TaskRepository.Get(filter: t => t.TaskId.Equals(request.TaskId) && t.IsDeleted == false && t.Status == 0).FirstOrDefault();
            if (task == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Công việc không tồn tại");
            }

            try
            {
                foreach (var assignedToId in request.AssignedToIds)
                {
                    var existEmployee = _unitOfWork.UserRepository.Get(filter: u => u.UserId.Equals(assignedToId) && u.FarmEmployees.Any(fe => fe.FarmId.Equals(task.FarmId)) && u.Status == 1, includeProperties: "FarmEmployees").FirstOrDefault();
                    if (existEmployee == null)
                    {
                        return BaseResponse<bool>.FailureResponse(message: "Người dùng không tồn tại");
                    }

                    var assignment = new Assignment
                    {
                        TaskId = request.TaskId,
                        AssignedDate = DateTime.Now.ToLocalTime(),
                        AssignedToId = assignedToId,
                        Note = request.Note,
                        Status = request.Status,
                    };

                    var noti = new Notification
                    {
                        UserId = assignedToId,
                        NotificationName = "Thông báo giao việc",
                        NotificationType = "ASSIGNMENT_TASK",
                        Content = "Công việc " + task.TaskName + " đã được giao đến bạn, vui lòng hoàn thành đúng thời hạn",
                        IsRead = 0
                    };

                    _unitOfWork.NotificationRepository.Insert(noti);
                    _unitOfWork.AssignmentRepository.Insert(assignment);
                }

                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Tạo thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Tạo không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra");
            }
        }
    }
}
