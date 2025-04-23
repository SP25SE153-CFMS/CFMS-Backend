using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CFMS.Application.Features.RequestFeat.GetRequest
{
    public class GetRequestQueryHandler : IRequestHandler<GetRequestQuery, BaseResponse<Request>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetRequestQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<Request>> Handle(GetRequestQuery request, CancellationToken cancellationToken)
        {
            var existRequest = _unitOfWork.RequestRepository.GetIncludeMultiLayer(filter: f => f.RequestId.Equals(request.Id) && f.IsDeleted == false,
            include: x => x
                .Include(r => r.InventoryRequests)
                    .ThenInclude(r => r.InventoryRequestDetails)
                        .ThenInclude(r => r.Resource)
                            .ThenInclude(r => r.Food)
                .Include(r => r.InventoryRequests)
                    .ThenInclude(r => r.InventoryRequestDetails)
                        .ThenInclude(r => r.Resource)
                            .ThenInclude(r => r.Equipment)
                .Include(r => r.InventoryRequests)
                    .ThenInclude(r => r.InventoryRequestDetails)
                        .ThenInclude(r => r.Resource)
                            .ThenInclude(r => r.Medicine)
                .Include(r => r.InventoryRequests)
                    .ThenInclude(r => r.InventoryRequestDetails)
                        .ThenInclude(r => r.Resource)
                            .ThenInclude(r => r.Chicken)
                .Include(r => r.InventoryRequests)
                    .ThenInclude(r => r.InventoryRequestDetails)
                        .ThenInclude(r => r.Resource)
                            .ThenInclude(r => r.HarvestProduct)
                .Include(r => r.InventoryRequests)
                    .ThenInclude(r => r.WareFrom)
                        .ThenInclude(r => r.Farm)
                .Include(r => r.InventoryRequests)
                    .ThenInclude(r => r.WareTo)
                        .ThenInclude(r => r.Farm)
                .Include(r => r.TaskRequests)
                .Include(r => r.InventoryRequests)
                    .ThenInclude(r => r.InventoryReceipts)
                        .ThenInclude(r => r.InventoryReceiptDetails)
                ).FirstOrDefault();
            if (existRequest == null)
            {
                return BaseResponse<Request>.SuccessResponse(message: "Phiếu yêu cầu không tồn tại");
            }

            return BaseResponse<Request>.SuccessResponse(data: existRequest);
        }
    }
}
