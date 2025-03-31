using AutoMapper;
using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CFMS.Application.Features.RequestFeat.Update
{
    public class UpdateRequestCommandHandler : IRequestHandler<UpdateRequestCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public UpdateRequestCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<BaseResponse<bool>> Handle(UpdateRequestCommand request, CancellationToken cancellationToken)
        {
            var user = _currentUserService.GetUserId();

            var existingRequest = _unitOfWork.RequestRepository
                .Get(filter: r => r.RequestId.Equals(request.RequestId),
                includeProperties: [
                    r => r.InventoryRequests,
                    r => r.TaskRequests
                    ]).FirstOrDefault();

            if (existingRequest == null)
                return BaseResponse<bool>.FailureResponse("Phiếu không tồn tại");

            existingRequest.RequestTypeId = request.RequestTypeId ?? existingRequest.RequestTypeId;
            existingRequest.Status = request.Status ?? existingRequest.Status;

            if (request.IsInventoryRequest)
            {
                var inventoryRequest = await _unitOfWork.InventoryRequestRepository
                    .FirstOrDefaultAsync(ir => ir.RequestId.Equals(request.RequestId));

                if (inventoryRequest != null)
                {
                    inventoryRequest.InventoryRequestTypeId = request.InventoryRequestTypeId ?? inventoryRequest.InventoryRequestTypeId;
                    inventoryRequest.WareFromId = request.WareFromId ?? inventoryRequest.WareFromId;
                    inventoryRequest.WareToId = request.WareToId ?? inventoryRequest.WareToId;

                    var existingDetails = _unitOfWork.InventoryRequestDetailRepository
                        .Get(filter: d => d.InventoryRequestId.Equals(inventoryRequest.InventoryRequestId));

                    _unitOfWork.InventoryRequestDetailRepository.DeleteRange(existingDetails);

                    foreach (var detail in request.InventoryDetails)
                    {
                        var inventoryRequestDetail = new InventoryRequestDetail
                        {
                            InventoryRequestId = inventoryRequest.InventoryRequestId,
                            ResourceId = detail.ResourceId,
                            ExpectedQuantity = detail.ExpectedQuantity,
                            UnitId = detail.UnitId,
                            Reason = detail.Reason,
                            ExpectedDate = detail.ExpectedDate,
                            Note = detail.Note
                        };
                        _unitOfWork.InventoryRequestDetailRepository.Insert(inventoryRequestDetail);
                    }
                }
            }
            else
            {
                var taskRequest = await _unitOfWork.TaskRequestRepository
                    .FirstOrDefaultAsync(tr => tr.RequestId.Equals(request.RequestId));

                if (taskRequest != null)
                {
                    taskRequest.TaskTypeId = request.TaskTypeId ?? taskRequest.TaskTypeId;
                    taskRequest.Priority = request.Priority ?? taskRequest.Priority;
                    taskRequest.Description = request.Description ?? taskRequest.Description;
                }
            }

            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0
                ? BaseResponse<bool>.SuccessResponse("Cập nhật phiếu thành công")
                : BaseResponse<bool>.FailureResponse("Cập nhật thất bại");
        }
    }
}
