using AutoMapper;
using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.ChickenFeat.Create
{
    public class CreateChickenCommandHandler : IRequestHandler<CreateChickenCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateChickenCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<bool>> Handle(CreateChickenCommand request, CancellationToken cancellationToken)
        {
            var existBatch = _unitOfWork.ChickenBatchRepository.Get(filter: b => b.ChickenBatchId.Equals(request.ChickenBatchId) && b.IsDeleted == false).FirstOrDefault();
            if (existBatch == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Lứa không tồn tại");
            }

            var existChicken = _unitOfWork.ChickenRepository.Get(c => c.ChickenCode.Equals(request.ChickenCode) || c.ChickenName.Equals(request.ChickenName) && c.IsDeleted == false).FirstOrDefault();
            if (existChicken != null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Tên hoặc Code đã được sử dụng");
            }

            try
            {
                _unitOfWork.ChickenRepository.Insert(_mapper.Map<Chicken>(request));
                await _unitOfWork.SaveChangesAsync();

                existChicken = _unitOfWork.ChickenRepository.Get(filter: p => p.ChickenName.Equals(request.ChickenName) && p.IsDeleted == false).FirstOrDefault();

                var chickenDetails = request.ChickenDetails.Select(detail => new ChickenDetail
                {
                    ChickenId = existChicken.ChickenId,
                    Weight = detail.Weight,
                    Quantity = detail.Quantity,
                    Gender = detail.Gender
                }).ToList() ?? new List<ChickenDetail>();

                _unitOfWork.ChickenDetailRepository.InsertRange(chickenDetails);

                var result = await _unitOfWork.SaveChangesAsync();

                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Thêm thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Thêm không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra");
            }
        }
    }
}
