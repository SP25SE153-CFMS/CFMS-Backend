using CFMS.Application.Common;
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
    public class GetReceiptByRequestIdQueryHandler : IRequestHandler<GetReceiptByRequestIdQuery, BaseResponse<IEnumerable<InventoryReceipt>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetReceiptByRequestIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<IEnumerable<InventoryReceipt>>> Handle(GetReceiptByRequestIdQuery request, CancellationToken cancellationToken)
        {
            var existRequest = _unitOfWork.InventoryRequestRepository.GetIncludeMultiLayer(filter: f => f.InventoryRequestId.Equals(request.InventoryRequestId) && f.IsDeleted == false).FirstOrDefault();
            
            if (existRequest == null)
            {
                return BaseResponse<IEnumerable<InventoryReceipt>>.FailureResponse(message: "Phiếu yêu cầu không tồn tại");
            }

            var existReceipts = _unitOfWork.InventoryReceiptRepository.GetIncludeMultiLayer(filter: f => f.InventoryRequestId.Equals(request.InventoryRequestId) && f.IsDeleted == false,
                include: x => x
                .Include(r => r.InventoryReceiptDetails)
                ).ToList();

            return BaseResponse<IEnumerable<InventoryReceipt >>.SuccessResponse(data: existReceipts);
        }
    }
}
