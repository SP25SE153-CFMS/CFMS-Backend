using CFMS.Application.Common;
using CFMS.Application.Features.RequestFeat.Create;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CFMS.Application.Features.RequestFeat.ApproveRequest
{
    public class ApproveRequestCommandHandler : IRequestHandler<ApproveRequestCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public ApproveRequestCommandHandler(ICurrentUserService currentUserService, IUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(ApproveRequestCommand request, CancellationToken cancellationToken)
        {
            var existRequest = _unitOfWork.RequestRepository.Get(filter: f => f.RequestId.Equals(request.RequestId) && f.IsDeleted == false).FirstOrDefault();
            if (existRequest == null)
            {
                return BaseResponse<bool>.SuccessResponse(message: "Phiếu yêu cầu không tồn tại");
            }

            var user = _currentUserService.GetUserId();
            int isApproved = request.IsApproved;

            existRequest.Status = isApproved.Equals(2) ? 2 : 1;

            if (Guid.TryParse(user, out Guid id))
            {
                existRequest.ApprovedById = id;
            }
            else
            {
                existRequest.ApprovedById = existRequest.ApprovedById;
            }

            _unitOfWork.RequestRepository.Update(existRequest);
            await _unitOfWork.SaveChangesAsync();

            return existRequest.Status.Equals(2) 
                ? BaseResponse<bool>.SuccessResponse("Duyệt thành công") 
                : BaseResponse<bool>.SuccessResponse("Từ chối thành công");
        }
    }
}
