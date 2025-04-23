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
                return BaseResponse<bool>.FailureResponse(message: "Trang trại không tồn tại");
            }

            var farms = _unitOfWork.FarmRepository.Get(filter: f => f.FarmCode.Equals(request.FarmCode) && f.IsDeleted == false && f.FarmId != request.FarmId);
            if (farms.Any())
            {
                return BaseResponse<bool>.FailureResponse(message: "Mã trang trại đã tồn tại");
            }

            try
            {
                existFarm.FarmName = request.FarmName;
                existFarm.FarmCode = request.FarmCode;
                existFarm.Area = request.Area;
                existFarm.Address = request.Address;
                existFarm.PhoneNumber = request.PhoneNumber;
                existFarm.Scale = request.Scale;
                existFarm.Website = request.Website;
                existFarm.ImageUrl = request.ImageUrl;
                existFarm.Longitude = request.Longitude;
                existFarm.Latitude = request.Latitude;

                _unitOfWork.FarmRepository.Update(existFarm);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Cập nhật thành công");
                }
                return BaseResponse<bool>.SuccessRFailureResponseesponse(message: "Cập nhật không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra:" + ex.Message);
            }
        }
    }
}
