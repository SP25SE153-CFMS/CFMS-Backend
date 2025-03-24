using AutoMapper;
using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.BreedingAreaFeat.Create
{
    public class CreateBreedingAreaCommandHandler : IRequestHandler<CreateBreedingAreaCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateBreedingAreaCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<bool>> Handle(CreateBreedingAreaCommand request, CancellationToken cancellationToken)
        {
            var existFarm = _unitOfWork.FarmRepository.GetByID(request.FarmId);
            if (existFarm == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Trang trại không tồn tại");
            }

            var existBreedingArea = _unitOfWork.BreedingAreaRepository.Get(filter: ba => ba.BreedingAreaCode.Equals(request.BreedingAreaCode) && ba.IsDeleted == false);
            if (existBreedingArea == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Mã khu nuôi đã tồn tại");
            }

            try
            {
                _unitOfWork.BreedingAreaRepository.Insert(_mapper.Map<BreedingArea>(request));
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Tạo Khu nuôi thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Tạo khu nuôi thất bại");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra:" + ex.Message);
            }
        }
    }
}
