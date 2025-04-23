using CFMS.Application.Common;
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
    public class GetReceiptQueryHandler : IRequestHandler<GetReceiptQuery, BaseResponse<InventoryReceipt>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetReceiptQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<InventoryReceipt>> Handle(GetReceiptQuery request, CancellationToken cancellationToken)
        {
            var existReceipt = _unitOfWork.InventoryReceiptRepository.GetIncludeMultiLayer(filter: f => f.InventoryReceiptId.Equals(request.Id) && f.IsDeleted == false,
                include: x => x
                .Include(r => r.InventoryReceiptDetails)
                ).FirstOrDefault();
            if (existReceipt == null)
            {
                return BaseResponse<InventoryReceipt>.FailureResponse(message: "Phiếu yêu cầu không tồn tại");
            }

            return BaseResponse<InventoryReceipt>.SuccessResponse(data: existReceipt);
        }
    }
}
