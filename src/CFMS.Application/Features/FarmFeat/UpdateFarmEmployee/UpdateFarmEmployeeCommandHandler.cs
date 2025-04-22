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
                return BaseResponse<bool>.FailureResponse(message: "Trang trại không tồn tại");
            }

            var existFarmEmployee = _unitOfWork.FarmEmployeeRepository.Get(u => u.FarmEmployeeId.Equals(request.FarmEmployeeId)).FirstOrDefault();
            if (existFarmEmployee == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Người dùng không làm việc trong trang trại này");
            }

            try
            {
                bool isUpdateFunc = true;

                if (request.StartDate == null 
                    && request.EndDate == null 
                    && request.FarmRole == null 
                    && request.Mail == null
                    && request.PhoneNumber == null
                    && (request.Status.Equals(2) || request.Status.Equals(0)))
                {
                    isUpdateFunc = false;
                }

                existFarmEmployee.StartDate = isUpdateFunc ? request.StartDate : existFarmEmployee.StartDate;
                existFarmEmployee.EndDate = isUpdateFunc ? request.EndDate
                                                         : request.Status.Equals(2)
                                                            ? DateTime.Now.ToLocalTime().AddHours(7)
                                                            : existFarmEmployee.EndDate;
                existFarmEmployee.Status = request.Status;
                existFarmEmployee.Mail = isUpdateFunc ? request.Mail : existFarmEmployee.Mail;
                existFarmEmployee.PhoneNumber = isUpdateFunc ? request.PhoneNumber : existFarmEmployee.PhoneNumber;
                existFarmEmployee.FarmRole = isUpdateFunc ? request.FarmRole : existFarmEmployee.FarmRole;

                _unitOfWork.FarmEmployeeRepository.Update(existFarmEmployee);
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
