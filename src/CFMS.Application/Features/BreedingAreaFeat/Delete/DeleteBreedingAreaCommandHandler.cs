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
            var existFarm = _unitOfWork.BreedingAreaRepository.GetByID(request.Id);
            if (existFarm == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Farm không tồn tại");
            }

            try
            {
                _unitOfWork.FarmRepository.Delete(existFarm);
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
