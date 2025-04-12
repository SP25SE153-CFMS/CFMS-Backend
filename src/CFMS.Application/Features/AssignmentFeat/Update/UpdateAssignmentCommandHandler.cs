using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.AssignmentFeat.Update
{
    public class UpdateAssignmentCommandHandler : IRequestHandler<UpdateAssignmentCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAssignmentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(UpdateAssignmentCommand request, CancellationToken cancellationToken)
        {
            var task = _unitOfWork.TaskRepository.Get(filter: t => t.TaskId.Equals(request.TaskId) && t.IsDeleted == false).FirstOrDefault();
            if (task == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Task không tồn tại");
            }

            var existAssignment = _unitOfWork.AssignmentRepository.Get(filter: a => a.AssignmentId.Equals(request.AssignmentId) && a.IsDeleted == false).FirstOrDefault();
            if (existAssignment == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Assignment không tồn tại");
            }

            try
            {
                existAssignment.AssignedDate = request.AssignedDate;
                existAssignment.Note = request.Note;
                existAssignment.Status = request.Status;

                var noti = new Notification
                {
                    UserId = existAssignment.AssignedToId,
                    NotificationName = "Thông báo cập nhật giao việc",
                    NotificationType = "ASSIGNMENT_UPDATE",
                    Content = "Công việc " + task.TaskName + " được giao đến bạn đã cập nhật, vui lòng kiểm tra lại thông tin",
                    IsRead = 0
                };

                _unitOfWork.NotificationRepository.Insert(noti);
                _unitOfWork.AssignmentRepository.Insert(existAssignment);

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
