using AutoMapper;
using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Commands.FarmFeat.Create
{
    public class CreateFarmCommandHandler : IRequestHandler<CreateFarmCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateFarmCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<bool>> Handle(CreateFarmCommand request, CancellationToken cancellationToken)
        {
            var existUser = _unitOfWork.UserRepository.Get(filter: u => u.UserId.Equals(request.OwnerId));
            if (existUser == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "User không tồn tại");
            }

            var farms = _unitOfWork.FarmRepository.Get(filter: f => f.FarmCode.Equals(request.FarmCode));
            if (farms.Any())
            {
                return BaseResponse<bool>.FailureResponse(message: "FarmCode đã tồn tại");
            }

            try
            {
                _unitOfWork.FarmRepository.Insert(_mapper.Map<Farm>(request));
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Tạo Farm thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Tạo Farm không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra");
            }
        }
    }
}
