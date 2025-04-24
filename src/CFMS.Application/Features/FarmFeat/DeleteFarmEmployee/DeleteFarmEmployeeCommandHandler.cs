using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.FarmFeat.DeleteFarmEmployee
{
    public class DeleteFarmEmployeeCommandHandler : IRequestHandler<DeleteFarmEmployeeCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteFarmEmployeeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteFarmEmployeeCommand request, CancellationToken cancellationToken)
        {
            var existFarmEmployee = _unitOfWork.FarmEmployeeRepository.Get(u => u.FarmEmployeeId.Equals(request.FarmEmployeeId) && u.FarmRole != 5).FirstOrDefault();
            if (existFarmEmployee == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Người dùng không làm việc trong trang trại này");
            }

            try
            {
                _unitOfWork.FarmEmployeeRepository.Delete(existFarmEmployee);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.FailureResponse(message: "Xóa thành công");
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
