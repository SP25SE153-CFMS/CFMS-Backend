using CFMS.Application.Common;
using CFMS.Application.Features.RequestFeat.GetRequestByFarmId;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.RequestFeat.GetReceiptByFarmId
{
    public class GetReceiptByFarmIdQueryHandler : IRequestHandler<GetReceiptByFarmIdQuery, BaseResponse<IEnumerable<InventoryReceipt>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetReceiptByFarmIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<IEnumerable<InventoryReceipt>>> Handle(GetReceiptByFarmIdQuery request, CancellationToken cancellationToken)
        {
            var existFarm = _unitOfWork.FarmRepository.Get(filter: f => f.FarmId.Equals(request.FarmId) && !f.IsDeleted).FirstOrDefault();
            if (existFarm == null)
            {
                return BaseResponse<IEnumerable<InventoryReceipt>>.FailureResponse(message: "Trang trại không tồn tại");
            }

            var existReceipt = _unitOfWork.InventoryReceiptRepository.GetIncludeMultiLayer(filter: f => f.FarmId.Equals(request.FarmId) && f.IsDeleted == false,
                include: x => x
                .Include(r => r.InventoryReceiptDetails),
                orderBy: q => q.OrderByDescending(x => x.CreatedWhen)
                ).ToList();

            return BaseResponse<IEnumerable<InventoryReceipt>>.SuccessResponse(data: existReceipt);
        }
    }
}
