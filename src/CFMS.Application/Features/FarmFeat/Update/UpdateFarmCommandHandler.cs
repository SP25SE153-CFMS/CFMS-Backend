using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.FarmFeat.Update
{
    public class UpdateFarmCommandHandler : IRequestHandler<UpdateFarmCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateFarmCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(UpdateFarmCommand request, CancellationToken cancellationToken)
        {
            var existFarm = _unitOfWork.FarmRepository.GetByID(request.FarmId);
            if (existFarm == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Farm không tồn tại");
            }

            try
            {
                existFarm.FarmName = request.FarmName;
                existFarm.FarmCode = request.FarmCode;
                existFarm.Area = request.Area;
                existFarm.Address = request.Address;
                existFarm.FarmImage = request.FarmImage;
                existFarm.PhoneNumber = request.PhoneNumber;
                existFarm.PhoneNumber = request.PhoneNumber;
                existFarm.Scale = request.Scale;
                existFarm.Type = request.Type;

                _unitOfWork.FarmRepository.Update(existFarm);
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
