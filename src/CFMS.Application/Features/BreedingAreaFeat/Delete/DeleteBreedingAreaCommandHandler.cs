using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.BreedingAreaFeat.Delete
{
    public class DeleteBreedingAreaCommandHandler : IRequestHandler<DeleteBreedingAreaCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBreedingAreaCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteBreedingAreaCommand request, CancellationToken cancellationToken)
        {
            var existBreeding = _unitOfWork.BreedingAreaRepository.Get(filter: b => b.BreedingAreaId.Equals(request.Id) && b.IsDeleted == false).FirstOrDefault();
            if (existBreeding == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Khu nuôi không tồn tại");
            }

            try
            {
                _unitOfWork.FarmRepository.Delete(existBreeding);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Xóa thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Xoá không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra");
            }
        }
    }
}
