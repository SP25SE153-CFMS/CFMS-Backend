using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.DTOs.Auth;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.UserFeat.GetUserByCCCDByPhoneByEmail
{
    public class GetUserByCCCDByPhoneByEmailQueryHandler : IRequestHandler<GetUserByCCCDByPhoneByEmailQuery, BaseResponse<UserResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserByCCCDByPhoneByEmailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<UserResponse>> Handle(GetUserByCCCDByPhoneByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = _unitOfWork.UserRepository.Get(filter: u => u.Cccd.Equals(request.SearchTemp) || u.Mail.Equals(request.SearchTemp) || u.PhoneNumber.Equals(request.SearchTemp)).FirstOrDefault();
            UserResponse userResponse = new UserResponse();
            if (user is not null)
            {
                userResponse = _mapper.Map<UserResponse>(user);
            }
            return BaseResponse<UserResponse>.SuccessResponse(data: userResponse);
        }
    }
}
