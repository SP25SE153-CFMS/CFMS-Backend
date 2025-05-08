using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CFMS.Application.Features.FarmFeat.AddFarmEmployee
{
    public class AddFarmEmployeeCommandHandler : IRequestHandler<AddFarmEmployeeCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddFarmEmployeeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(AddFarmEmployeeCommand request, CancellationToken cancellationToken)
        {
            var existFarm = _unitOfWork.FarmRepository.Get(filter: f => f.FarmId.Equals(request.FarmId) && f.IsDeleted == false).FirstOrDefault();
            if (existFarm == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Trang trại không tồn tại");
            }

            var existUser = _unitOfWork.UserRepository.Get(u => u.UserId.Equals(request.UserId) && u.Status == 1).FirstOrDefault();
            if (existUser == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Người dùng không tồn tại");
            }

            var existEmployee = _unitOfWork.FarmEmployeeRepository.GetIncludeMultiLayer(filter: fe => fe.UserId.Equals(request.UserId) && fe.FarmId.Equals(request.FarmId) && fe.IsDeleted == false,
                include: x => x
                .Include(t => t.User)
                ).FirstOrDefault();
            if (existEmployee != null)
            {
                return BaseResponse<bool>.FailureResponse(message:
                    $"{existEmployee.Status switch
                    {
                        0 => "Người dùng đang bị đình chỉ hoặc tạm ngưng làm việc trong trang trại này",
                        1 => "Người dùng này đang làm việc trong trang trại này",
                        //2 => "Người dùng này dã từng bị sa thải. Có chắc chắn muốn thêm không?"
                        _ => "Người dùng này có trạng thái không xác định"
                    }}");
            }

            try
            {
                existFarm.FarmEmployees.Add(new FarmEmployee
                {
                    FarmId = request.FarmId,
                    UserId = request.UserId,
                    StartDate = DateTime.Now.ToLocalTime().AddHours(7),
                    Status = (request.Status > 0 && request.Status < 6) ? request.Status : 3,
                    FarmRole = request.FarmRole,
                });

                _unitOfWork.FarmRepository.Update(existFarm);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Thêm thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Thêm không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra");
            }
        }
    }
}
