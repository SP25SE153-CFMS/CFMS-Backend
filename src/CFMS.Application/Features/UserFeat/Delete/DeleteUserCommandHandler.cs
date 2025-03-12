using CFMS.Application.Common;
using CFMS.Application.Features.FarmFeat.Delete;
using CFMS.Application.Services;
using CFMS.Domain.Enums.Status;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.UserFeat.Delete
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;

        public DeleteUserCommandHandler(IUnitOfWork unitOfWork, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                var existUser = _unitOfWork.UserRepository.GetByID(request.UserId);
                if (existUser == null) return BaseResponse<bool>.FailureResponse("Người dùng không tồn tại");
                existUser.Status = UserStatus.Fired.ToString();
                _unitOfWork.UserRepository.Update(existUser);

                var tokens = _unitOfWork.RevokedTokenRepository.Get(
                    filter: x => x.UserId.Equals(request.UserId))
                .ToList();

                foreach (var token in tokens)
                {
                    await _tokenService.RevokeRefreshTokenAsync(token);
                }

                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0
                    ? BaseResponse<bool>.SuccessResponse("Xóa thành công")
                    : BaseResponse<bool>.FailureResponse("Xóa không thành công");
            });
        }
    }
}
