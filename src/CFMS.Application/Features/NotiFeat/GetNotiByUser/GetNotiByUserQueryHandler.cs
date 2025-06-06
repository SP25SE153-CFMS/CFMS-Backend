﻿using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CFMS.Application.Features.NotiFeat.GetNotiByUser
{
    public class GetNotiByUserQueryHandler : IRequestHandler<GetNotiByUserQuery, BaseResponse<IEnumerable<Notification>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public GetNotiByUserQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<BaseResponse<IEnumerable<Notification>>> Handle(GetNotiByUserQuery request, CancellationToken cancellationToken)
        {
            var existUser = _unitOfWork.UserRepository.Get(filter: u => u.UserId.Equals(Guid.Parse(_currentUserService.GetUserId()))).FirstOrDefault();
            if (existUser == null)
            {
                return BaseResponse<IEnumerable<Notification>>.FailureResponse(message: "User không tồn tại");
            }

            var notis = _unitOfWork.NotificationRepository.GetIncludeMultiLayer(filter: n => n.UserId.Equals(existUser.UserId) && n.IsDeleted == false,
                include: x => x
                .Include(t => t.User)
                .Include(t => t.CreatedByUser
                ), orderBy: x => x.OrderByDescending(x => x.CreatedWhen));
            return BaseResponse<IEnumerable<Notification>>.SuccessResponse(data: notis);
        }
    }
}
