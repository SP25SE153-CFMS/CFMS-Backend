using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.FarmFeat.UpdateFarmEmployee
{
    public class UpdateFarmEmployeeCommandHandler : IRequestHandler<UpdateFarmEmployeeCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateFarmEmployeeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(UpdateFarmEmployeeCommand request, CancellationToken cancellationToken)
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
                exsitFarmEmployee.StartDate = request.StartDate;
                exsitFarmEmployee.EndDate = request.EndDate;
                exsitFarmEmployee.Status = request.Status;
                exsitFarmEmployee.FarmRole = request.FarmRole;

                _unitOfWork.FarmEmployeeRepository.Update(exsitFarmEmployee);
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
