using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.Events;
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
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;

        public CreateFarmCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IUtilityService utilityService, IMediator mediator, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _utilityService = utilityService;
            _mediator = mediator;
            _currentUserService = currentUserService;
        }

        public async Task<BaseResponse<bool>> Handle(CreateFarmCommand request, CancellationToken cancellationToken)
        {
            var ownerId = Guid.Parse(_currentUserService.GetUserId());
            var existUser = _unitOfWork.UserRepository.Get(filter: u => u.UserId.Equals(ownerId) && u.Status == 1).FirstOrDefault();
            if (existUser == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "User không tồn tại");
            }

            var farms = _unitOfWork.FarmRepository.Get(filter: f => f.FarmCode.Equals(request.FarmCode) && f.IsDeleted == false);
            if (farms.Any())
            {
                return BaseResponse<bool>.FailureResponse(message: "Mã trang trại đã tồn tại");
            }

            try
            {
                var farm = _mapper.Map<Farm>(request);
                farm.FarmEmployees.Add(new FarmEmployee
                {
                    UserId = existUser.UserId,
                    Mail = existUser.Mail,
                    PhoneNumber = existUser.PhoneNumber,
                    FarmRole = 5,
                    StartDate = DateTime.Now.ToLocalTime()
                });

                _unitOfWork.FarmRepository.Insert(farm);
                var result = await _unitOfWork.SaveChangesAsync();

                var existFarm = _unitOfWork.FarmRepository.Get(filter: f => f.FarmCode.Equals(request.FarmCode) && f.FarmName.Equals(request.FarmName) && f.IsDeleted == false).FirstOrDefault();
                if (existFarm == null)
                {
                    return BaseResponse<bool>.FailureResponse(message: "Không tìm thấy trang trại");
                }

                await _mediator.Publish(new WareCreatedEvent(existFarm.FarmId));
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
