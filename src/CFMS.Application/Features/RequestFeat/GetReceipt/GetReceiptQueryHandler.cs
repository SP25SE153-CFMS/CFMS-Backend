using CFMS.Application.Common;
using CFMS.Application.DTOs.Receipt;
using CFMS.Application.Features.RequestFeat.GetRequest;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.RequestFeat.GetReceipt
{
    public class GetReceiptQueryHandler : IRequestHandler<GetReceiptQuery, BaseResponse<ReceiptResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetReceiptQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<ReceiptResponse>> Handle(GetReceiptQuery request, CancellationToken cancellationToken)
        {
            var existReceipt = _unitOfWork.InventoryReceiptRepository.GetIncludeMultiLayer(filter: f => f.InventoryReceiptId.Equals(request.Id) && f.IsDeleted == false,
                include: x => x
                .Include(r => r.InventoryReceiptDetails),
                orderBy: q => q.OrderByDescending(x => x.CreatedWhen)
                ).ToList()
                .Select(r =>
                {
                    var inventoryReq = _unitOfWork.InventoryRequestRepository.GetIncludeMultiLayer(filter: f => f.InventoryRequestId.Equals(r.InventoryRequestId) && !f.IsDeleted,
                        include: x => x
                        .Include(i => i.InventoryRequestDetails)
                        ).FirstOrDefault();

                    return new ReceiptResponse
                    {
                        InventoryReceiptId = r.InventoryReceiptId,
                        InventoryRequestId = r.InventoryRequestId,
                        ReceiptTypeId = r.ReceiptTypeId,
                        ReceiptCodeNumber = r.ReceiptCodeNumber,
                        BatchNumber = r.BatchNumber,
                        FarmId = r.FarmId,
                        WareFromId = inventoryReq?.WareFromId,
                        WareToId = inventoryReq?.WareToId,
                        InventoryReceiptDetails = r.InventoryReceiptDetails,
                        UserId = r.CreatedByUserId
                    };
                }).FirstOrDefault();

            if (existReceipt == null)
            {
                return BaseResponse<ReceiptResponse>.FailureResponse(message: "Phiếu yêu cầu không tồn tại");
            }

            return BaseResponse<ReceiptResponse>.SuccessResponse(data: existReceipt);
        }
    }
}
