using AutoMapper;
using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.Create
{
    public class CreateChickenBatchCommandHandler : IRequestHandler<CreateChickenBatchCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateChickenBatchCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<bool>> Handle(CreateChickenBatchCommand request, CancellationToken cancellationToken)
        {
            var existCoop = _unitOfWork.ChickenCoopRepository.Get(filter: c => c.ChickenCoopId.Equals(request.ChickenCoopId) && c.IsDeleted == false).FirstOrDefault();
            if (existCoop == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Chuồng không tồn tại");
            }

            try
            {
                _unitOfWork.ChickenBatchRepository.Insert(_mapper.Map<ChickenBatch>(request));
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Tạo lứa thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Tạo lứa không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra:" + ex.Message);
            }
        }
    }
}
