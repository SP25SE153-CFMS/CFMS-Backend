using CFMS.Application.Common;
using CFMS.Application.DTOs.Request;
using CFMS.Application.Features.RequestFeat.GetReceipt;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.RequestFeat.GetReceiptByRequestId
{
    public class GetReceiptByRequestIdQueryHandler : IRequestHandler<GetReceiptByRequestIdQuery, BaseResponse<ReceiptDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetReceiptByRequestIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<ReceiptDto>> Handle(GetReceiptByRequestIdQuery request, CancellationToken cancellationToken)
        {
            var existRequest = _unitOfWork.InventoryRequestRepository.GetIncludeMultiLayer(filter: f => f.InventoryRequestId.Equals(request.InventoryRequestId) && f.IsDeleted == false).FirstOrDefault();
            
            if (existRequest == null)
            {
                return BaseResponse<ReceiptDto>.FailureResponse(message: "Phiếu yêu cầu không tồn tại");
            }

            var existReceipts = _unitOfWork.InventoryReceiptRepository.GetIncludeMultiLayer(filter: f => f.InventoryRequestId.Equals(request.InventoryRequestId) && f.IsDeleted == false,
                include: x => x
                .Include(r => r.InventoryReceiptDetails)
                ).ToList();

            var result = new ReceiptDto
            {
                AmountOfBatch = existReceipts.Count,
                AmountOfActualQuantity = existReceipts
                        .SelectMany(x => x.InventoryReceiptDetails)
                        .GroupBy(d => d.ResourceId)
                        .Select(g => new ActualQuantityDto
                        {
                            ResourceId = g.Key,
                            TotalQuantity = g.Sum(y => y.ActualQuantity ?? 0)
                        })
                        .ToList(),
                InventoryReceipts = existReceipts
            };

            return BaseResponse<ReceiptDto>.SuccessResponse(data: result);
        }
    }
}
