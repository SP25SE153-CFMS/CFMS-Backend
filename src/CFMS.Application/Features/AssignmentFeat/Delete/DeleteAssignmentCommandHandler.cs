using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.AssignmentFeat.Delete
{
    public class DeleteAssignmentCommandHandler : IRequestHandler<DeleteAssignmentCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAssignmentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteAssignmentCommand request, CancellationToken cancellationToken)
        {
            var existAssignment = _unitOfWork.AssignmentRepository.Get(filter: a => a.AssignmentId.Equals(request.AssignmentId) && a.IsDeleted == false).FirstOrDefault();
            if (existAssignment == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Assignment không tồn tại");
            }

            try
            {
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
