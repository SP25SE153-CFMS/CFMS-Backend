using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.ChickenCoopFeat.Update
{
    public class UpdateCoopCommandHandler : IRequestHandler<UpdateCoopCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCoopCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(UpdateCoopCommand request, CancellationToken cancellationToken)
        {
            var existCoop = _unitOfWork.ChickenCoopRepository.Get(filter: c => c.ChickenCoopId.Equals(request.Id) && c.IsDeleted == false).FirstOrDefault();
            if (existCoop == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Chuồng gà không tồn tại");
            }

            try
            {
                existCoop.ChickenCoopName = request.ChickenCoopName;
                existCoop.ChickenCoopCode = request.ChickenCoopCode;
                existCoop.CurrentQuantity = request.CurrentQuantity;
                existCoop.Description = request.Description;
                existCoop.MaxQuantity = request.MaxQuantity;
                existCoop.Density = request.Density;
                existCoop.Status = request.Status;
                existCoop.Area = request.Area;
                existCoop.PurposeId = request.PurposeId;
                existCoop.BreedingAreaId = request.BreedingAreaId;

                _unitOfWork.ChickenCoopRepository.Update(existCoop);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Cập nhật thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Cập nhật không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra:" + ex.Message);
            }
        }
    }
}
