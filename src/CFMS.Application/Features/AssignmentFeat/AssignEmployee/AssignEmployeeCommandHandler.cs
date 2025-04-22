using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.Services.SignalR;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using Org.BouncyCastle.Math.EC.Rfc7748;

namespace CFMS.Application.Features.AssignmentFeat.AssignEmployee
{
    public class AssignEmployeeCommandHandler : IRequestHandler<AssignEmployeeCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly NotiHub _hubContext;

        public AssignEmployeeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, NotiHub hubContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hubContext = hubContext;
        }

        public async Task<BaseResponse<bool>> Handle(AssignEmployeeCommand request, CancellationToken cancellationToken)
        {
            var task = _unitOfWork.TaskRepository.Get(filter: t => t.TaskId.Equals(request.TaskId) && t.IsDeleted == false && t.Status == 0).FirstOrDefault();
            if (task == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Công việc không tồn tại");
            }

            var chosenLeader = request.AssignedTos.Any(x => x.Status == 1);

            var isHaveLeader = task.Assignments.Any(x => x.Status == 1);

            if (!chosenLeader && !isHaveLeader)
            {
                return BaseResponse<bool>.FailureResponse(message: "Công việc này chưa có đội trưởng đảm nhận");
            }

            if (chosenLeader && isHaveLeader)
            {
                return BaseResponse<bool>.FailureResponse(message: "Công việc này đã có đội trưởng đảm nhận");
            }

            var existUserAssigned = task.Assignments.Any(x => request.AssignedTos.Select(t => t.AssignedToId).Contains(x.AssignedToId ?? Guid.Empty));
            if (existUserAssigned)
            {
                return BaseResponse<bool>.FailureResponse(message: "Người dùng đã được giao công việc này");
            }

            try
            {
                foreach (var assignedTo in request.AssignedTos)
                {
                    var existEmployee = _unitOfWork.UserRepository.Get(filter: u => u.UserId.Equals(assignedTo.AssignedToId) && u.FarmEmployees.Any(fe => fe.FarmId.Equals(task.FarmId)) && u.Status == 1, includeProperties: "FarmEmployees").FirstOrDefault();
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
                        Content = "Công việc " + task.TaskName + " đã được giao đến bạn, vui lòng hoàn thành đúng thời hạn",
                        IsRead = 0
                    };

                    await _hubContext.SendMessage(noti);

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
