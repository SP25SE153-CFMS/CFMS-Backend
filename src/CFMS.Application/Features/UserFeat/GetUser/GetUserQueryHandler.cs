using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.DTOs.Auth;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.UserFeat.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, BaseResponse<UserResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<UserResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var existUser = _unitOfWork.UserRepository.GetByID(request.UserId);
            if (existUser == null)
            {
                return BaseResponse<UserResponse>.FailureResponse("Người dùng không tồn tại");
            }
            return BaseResponse<UserResponse>.SuccessResponse(_mapper.Map<UserResponse>(existUser));
        }
    }
}
