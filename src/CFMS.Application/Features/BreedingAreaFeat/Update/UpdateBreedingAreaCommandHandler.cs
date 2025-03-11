using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.BreedingAreaFeat.Update
{
    public class UpdateBreedingAreaCommandHandler : IRequestHandler<UpdateBreedingAreaCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBreedingAreaCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(UpdateBreedingAreaCommand request, CancellationToken cancellationToken)
        {
            var existBreedingArea = _unitOfWork.BreedingAreaRepository.GetByID(request.BreedingAreaId);
            if (existBreedingArea == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Khu nuôi không tồn tại");
            }

            try
            {
                existBreedingArea.BreedingAreaName = request.BreedingAreaName;
                existBreedingArea.BreedingAreaCode = request.BreedingAreaCode;
                existBreedingArea.Image = request.Image;
                existBreedingArea.MealsPerDay = request.MealsPerDay;
                existBreedingArea.Notes = request.Notes;
                existBreedingArea.Area = request.Area;

                _unitOfWork.BreedingAreaRepository.Update(existBreedingArea);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Cập nhật thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Cập nhật thất bại");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra");
            }
        }
    }
}
