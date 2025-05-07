using AutoMapper;
using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.GrowthStageFeat.Create
{
    public class CreateStageCommandHandler : IRequestHandler<CreateStageCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateStageCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<bool>> Handle(CreateStageCommand request, CancellationToken cancellationToken)
        {
            var existChickenType = _unitOfWork.SubCategoryRepository.Get(filter: t => t.IsDeleted == false && t.SubCategoryId.Equals(request.ChickenType)).FirstOrDefault();
            if (existChickenType == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Loại gà không tồn tại");
            }

            var existStage = _unitOfWork.GrowthStageRepository.Get(filter: t => t.FarmId.Equals(request.FarmId) && t.IsDeleted == false && t.StageName.Equals(request.StageName)).FirstOrDefault();
            if (existStage != null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Tên giai đoạn phát triển đã tồn tại");
            }

            var groupStages = _unitOfWork.GrowthStageRepository.Get(
                filter: s => s.StageCode.Equals(request.StageCode) && s.IsDeleted == false && s.FarmId.Equals(request.FarmId),
                orderBy: s => s.OrderBy(s => s.MinAgeWeek)
            ).ToList();

            // 4. Kiểm tra khoảng tuần tuổi có chồng chéo với giai đoạn nào không
            foreach (var stage in groupStages)
            {
                bool isOverlap = !(request.MaxAgeWeek < stage.MinAgeWeek || request.MinAgeWeek > stage.MaxAgeWeek);
                if (isOverlap)
                {
                    return BaseResponse<bool>.FailureResponse($"Tuần tuổi {request.MinAgeWeek}-{request.MaxAgeWeek} bị chồng với giai đoạn '{stage.StageName}' ({stage.MinAgeWeek}-{stage.MaxAgeWeek})");
                }
            }

            try
            {
                _unitOfWork.GrowthStageRepository.Insert(_mapper.Map<GrowthStage>(request));
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Tạo thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Tạo không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra:" + ex.Message);
            }
        }
    }
}
