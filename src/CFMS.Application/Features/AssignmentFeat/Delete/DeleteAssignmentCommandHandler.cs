using CFMS.Application.Common;
using CFMS.Application.Services.SignalR;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.AssignmentFeat.Delete
{
    public class DeleteAssignmentCommandHandler : IRequestHandler<DeleteAssignmentCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly NotiHub _hubContext;

        public DeleteAssignmentCommandHandler(IUnitOfWork unitOfWork, NotiHub hubContext)
        {
            _unitOfWork = unitOfWork;
            _hubContext = hubContext;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteAssignmentCommand request, CancellationToken cancellationToken)
        {
            var existTask = _unitOfWork.TaskRepository.Get(filter: t => t.TaskId.Equals(request.TaskId) && t.IsDeleted == false).FirstOrDefault();
            if (existTask == null)
            {
                return BaseResponse<bool>.SuccessResponse(message: "Công việc không tồn tại");
            }

            var existAssignment = _unitOfWork.AssignmentRepository.Get(filter: a => a.AssignmentId.Equals(request.AssignmentId) && a.IsDeleted == false).FirstOrDefault();
            if (existAssignment == null)
            {
                return BaseResponse<bool>.SuccessResponse(message: "Phiên giao việc không tồn tại");
            }

            try
            {
                existTask.Assignments.Remove(existAssignment);

                var noti = new Notification
                {
                    UserId = existAssignment.AssignedToId,
                    NotificationName = "Thông báo hủy giao việc",
                    NotificationType = "ASSIGNMENT_REMOVE",
                    Content = "Công việc " + existTask.TaskName + " đã không còn giao cho bạn",
                    IsRead = 0
                };

                await _hubContext.SendMessage(noti);

                _unitOfWork.NotificationRepository.Insert(noti);
                _unitOfWork.TaskRepository.Update(existTask);
                _unitOfWork.AssignmentRepository.Delete(existAssignment);

                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Xóa thành công");
                }
                return BaseResponse<bool>.SuccessResponse(message: "Xóa không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra");
            }
        }
    }
}
