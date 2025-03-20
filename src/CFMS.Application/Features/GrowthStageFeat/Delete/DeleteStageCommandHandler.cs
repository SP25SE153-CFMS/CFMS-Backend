using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.GrowthStageFeat.Delete
{
    public class DeleteStageCommandHandler : IRequestHandler<DeleteStageCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteStageCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteStageCommand request, CancellationToken cancellationToken)
        {
            var existStage = _unitOfWork.GrowthStageRepository.Get(filter: f => f.GrowthStageId.Equals(request.Id) && f.IsDeleted == false).FirstOrDefault();
            if (existStage == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Stage không tồn tại");
            }

            try
            {
                _unitOfWork.GrowthStageRepository.Delete(existStage);
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
