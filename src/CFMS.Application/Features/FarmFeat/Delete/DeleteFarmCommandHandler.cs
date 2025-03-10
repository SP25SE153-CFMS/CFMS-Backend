using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.FarmFeat.Delete
{
    public class DeleteFarmCommandHandler : IRequestHandler<DeleteFarmCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteFarmCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteFarmCommand request, CancellationToken cancellationToken)
        {
            var existFarm = _unitOfWork.FarmRepository.GetByID(request.Id);
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
