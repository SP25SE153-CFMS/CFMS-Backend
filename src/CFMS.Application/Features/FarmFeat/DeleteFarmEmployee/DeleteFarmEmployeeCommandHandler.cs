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
            var existFarm = _unitOfWork.FarmRepository.Get(filter: f => f.FarmId.Equals(request.FarmId) && f.IsDeleted == false).FirstOrDefault();
            if (existFarm == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Farm không tồn tại");
            }

            var exsitFarmEmployee = _unitOfWork.FarmEmployeeRepository.Get(u => u.UserId.Equals(request.FarmEmployeeId) && u.Status == 0).FirstOrDefault();
            if (exsitFarmEmployee == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "User không tồn tại trong Farm");
            }

            try
            {
                existFarm.FarmEmployees.Remove(exsitFarmEmployee);

                _unitOfWork.FarmRepository.Update(existFarm);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Xóa thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Xóa không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra");
            }
        }
    }
}
