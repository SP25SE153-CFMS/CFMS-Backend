using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.DTOs.Auth;
using CFMS.Application.Features.FarmFeat.GetFarms;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.UserFeat.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, BaseResponse<IEnumerable<UserResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUsersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<IEnumerable<UserResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = _unitOfWork.UserRepository.Get();
            return BaseResponse<IEnumerable<UserResponse>>.SuccessResponse(_mapper.Map<IEnumerable<UserResponse>>(users));
        }
    }
}
