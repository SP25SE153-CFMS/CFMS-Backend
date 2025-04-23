using AutoMapper;
using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CFMS.Application.Features.RequestFeat.Delete
{
    public class DeleteRequestCommandHandler : IRequestHandler<DeleteRequestCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRequestCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteRequestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingRequest = _unitOfWork.RequestRepository
                    .Get(r => r.RequestId.Equals(request.Id),
                        includeProperties: [
                            r => r.InventoryRequests,
                        r => r.TaskRequests]).FirstOrDefault();

                if (existingRequest == null)
                {
                    return BaseResponse<bool>.SuccessResponse("Yêu cầu không tồn tại");
                }

                if (existingRequest.InventoryRequests != null)
                {
                    _unitOfWork.InventoryRequestRepository.DeleteRange(existingRequest.InventoryRequests);
                    await _unitOfWork.SaveChangesAsync();
                }

                if (existingRequest.TaskRequests != null)
                {
                    _unitOfWork.TaskRequestRepository.DeleteRange(existingRequest.TaskRequests);
                    await _unitOfWork.SaveChangesAsync();
                }

                _unitOfWork.RequestRepository.Delete(existingRequest);
                await _unitOfWork.SaveChangesAsync();
                return BaseResponse<bool>.SuccessResponse("Xóa yêu cầu thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse("Xóa thất bại: " + ex.Message);
            }
        }
    }
}
