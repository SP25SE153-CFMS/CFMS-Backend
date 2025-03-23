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
