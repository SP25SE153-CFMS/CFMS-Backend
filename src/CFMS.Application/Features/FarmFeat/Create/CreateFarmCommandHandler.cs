using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.Services.Impl;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.FarmFeat.Create
{
    public class CreateFarmCommandHandler : IRequestHandler<CreateFarmCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUtilityService _utilityService;

        public CreateFarmCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IUtilityService utilityService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _utilityService = utilityService;
        }

        public async Task<BaseResponse<bool>> Handle(CreateFarmCommand request, CancellationToken cancellationToken)
        {
            //var existUser = _unitOfWork.UserRepository.Get(filter: u => u.UserId.Equals(request.OwnerId));
            //if (existUser == null)
            //{
            //    return BaseResponse<bool>.FailureResponse(message: "User không tồn tại");
            //}

            var farms = _unitOfWork.FarmRepository.Get(filter: f => (f.FarmCode.Equals(request.FarmCode) || f.FarmName.Equals(request.FarmName)) && f.IsDeleted == false);
            if (farms.Any())
            {
                return BaseResponse<bool>.FailureResponse(message: "Tên hoặc mã trang trại đã tồn tại");
            }

            try
            {
                _unitOfWork.FarmRepository.Insert(_mapper.Map<Farm>(request));
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Tạo trang trại thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Tạo trang trại không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra:" + ex.Message);
            }
        }
    }
}
