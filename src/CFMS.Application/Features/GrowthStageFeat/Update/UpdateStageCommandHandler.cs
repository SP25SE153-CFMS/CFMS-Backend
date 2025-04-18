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
            var existStage = _unitOfWork.GrowthStageRepository
                 .Get(filter: s => s.GrowthStageId.Equals(request.Id) && s.IsDeleted == false)
                 .FirstOrDefault();

            if (existStage == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Giai đoạn phát triển không tồn tại");
            }

            // Lấy tất cả các stage thuộc cùng nhóm StageCode
            var groupStage = _unitOfWork.GrowthStageRepository.Get(
                filter: s => s.StageCode.Equals(existStage.StageCode) && s.IsDeleted == false,
                orderBy: s => s.OrderBy(s => s.MinAgeWeek)
            ).ToList();

            try
            {
                // 1. Cập nhật thông tin cho stage hiện tại
                existStage.StageName = request.StageName;
                existStage.Description = request.Description;
                existStage.ChickenType = request.ChickenType;
                existStage.MinAgeWeek = request.MinAgeWeek;
                existStage.MaxAgeWeek = request.MaxAgeWeek;

                // 2. Cập nhật lại các stage sau đó nếu cần
                bool found = false;
                for (int i = 0; i < groupStage.Count; i++)
                {
                    var stage = groupStage[i];

                    if (stage.GrowthStageId == request.Id)
                    {
                        found = true;
                        continue;
                    }

                    if (found)
                    {
                        var prevStage = groupStage[i - 1];
                        var gap = stage.MaxAgeWeek - stage.MinAgeWeek;
                        stage.MinAgeWeek = prevStage.MaxAgeWeek + 1;
                        stage.MaxAgeWeek = stage.MinAgeWeek + gap;

                        _unitOfWork.GrowthStageRepository.UpdateWithoutDetach(stage);
                    }
                }

                _unitOfWork.GrowthStageRepository.UpdateWithoutDetach(existStage);
                var result = await _unitOfWork.SaveChangesAsync();

                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Cập nhật thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Cập nhật không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra: " + ex.Message);
            }
        }
    }
}
