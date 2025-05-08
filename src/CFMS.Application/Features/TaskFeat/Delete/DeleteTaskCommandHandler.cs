using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.TaskFeat.Delete
{
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTaskCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var existTask = _unitOfWork.TaskRepository.Get(filter: t => t.TaskId.Equals(request.TaskId) && t.IsDeleted == false).FirstOrDefault();
            if (existTask == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Công việc không tồn tại");
            }

            try
            {
                _unitOfWork.TaskRepository.Delete(existTask);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Xóa thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Xoá không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra:" + ex.Message);
            }
        }
    }
}
