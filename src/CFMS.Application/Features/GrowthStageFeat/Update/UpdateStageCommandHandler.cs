using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.GrowthStageFeat.Update
{
    public class UpdateStageCommandHandler : IRequestHandler<UpdateStageCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateStageCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(UpdateStageCommand request, CancellationToken cancellationToken)
        {
            var existStage = _unitOfWork.GrowthStageRepository.Get(filter: s => s.GrowthStageId.Equals(request.Id) && s.IsDeleted == false).FirstOrDefault();
            if (existStage == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Stage không tồn tại");
            }

            try
            {
                existStage.StageName = request.StageName;
                existStage.Description = request.Description;
                existStage.ChickenType = request.ChickenType;
                existStage.MinAgeWeek = request.MinAgeWeek;
                existStage.MaxAgeWeek = request.MaxAgeWeek;

                _unitOfWork.GrowthStageRepository.Update(existStage);
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
