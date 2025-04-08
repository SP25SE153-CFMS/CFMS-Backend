using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

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
            var existUser = _unitOfWork.UserRepository.Get(filter: u => u.UserId.Equals(Guid.Parse(_currentUserService.GetUserId())) && u.Status == 1).FirstOrDefault();
            if (existUser == null)
            {
                return BaseResponse<IEnumerable<Notification>>.FailureResponse(message: "User không tồn tại");
            }

            var notis = _unitOfWork.NotificationRepository.Get(filter: n => n.UserId.Equals(existUser.UserId) && n.IsRead == 0, includeProperties: "User");
            return BaseResponse<IEnumerable<Notification>>.SuccessResponse(data: notis);
        }
    }
}
