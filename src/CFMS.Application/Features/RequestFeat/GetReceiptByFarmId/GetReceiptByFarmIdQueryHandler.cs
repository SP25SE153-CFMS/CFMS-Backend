using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.DTOs.Receipt;
using CFMS.Application.DTOs.Request;
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
    public class GetReceiptByFarmIdQueryHandler : IRequestHandler<GetReceiptByFarmIdQuery, BaseResponse<IEnumerable<ReceiptResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetReceiptByFarmIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<IEnumerable<ReceiptResponse>>> Handle(GetReceiptByFarmIdQuery request, CancellationToken cancellationToken)
        {
            var existFarm = _unitOfWork.FarmRepository.Get(filter: f => f.FarmId.Equals(request.FarmId) && !f.IsDeleted).FirstOrDefault();
            if (existFarm == null)
            {
                return BaseResponse<IEnumerable<ReceiptResponse>>.FailureResponse(message: "Trang trại không tồn tại");
            }

            var existReceipt = _unitOfWork.InventoryReceiptRepository.GetIncludeMultiLayer(filter: f => f.FarmId.Equals(request.FarmId) && f.IsDeleted == false,
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
                        InventoryReceiptDetails = r.InventoryReceiptDetails
                    };
                }).ToList();

            return BaseResponse<IEnumerable<ReceiptResponse>>.SuccessResponse(data: existReceipt);
        }
    }
}
