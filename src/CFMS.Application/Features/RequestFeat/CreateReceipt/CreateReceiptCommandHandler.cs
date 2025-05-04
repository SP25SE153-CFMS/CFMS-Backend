using CFMS.Application.Common;
using CFMS.Application.Events;
using CFMS.Application.Features.InventoryReceipts.Commands;
using CFMS.Domain.Entities;
using CFMS.Domain.Enums.Types;
using CFMS.Domain.Interfaces;
using CFMS.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class CreateInventoryReceiptCommandHandler : IRequestHandler<CreateInventoryReceiptCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;

    public CreateInventoryReceiptCommandHandler(IUnitOfWork unitOfWork, IMediator mediator)
    {
        _unitOfWork = unitOfWork;
        _mediator = mediator;
    }

    public async Task<BaseResponse<bool>> Handle(CreateInventoryReceiptCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var existRequest = _unitOfWork.RequestRepository.GetIncludeMultiLayer(x => x.RequestId == request.RequestId && !x.IsDeleted,
                include: x => x
                .Include(y => y.InventoryRequests)
                    .ThenInclude(z => z.InventoryRequestDetails)
                ).FirstOrDefault();

            if (existRequest == null)
                return BaseResponse<bool>.FailureResponse("Phiếu yêu cầu không tồn tại");

            if (existRequest.Status == 2)
                return BaseResponse<bool>.FailureResponse("Phiếu yêu cầu đã bị từ chối");

            if (existRequest.Status == 0)
                return BaseResponse<bool>.FailureResponse("Phiếu yêu cầu đang chờ duyệt");

            var existReceiptType = _unitOfWork.SubCategoryRepository.Get(x => x.SubCategoryId.Equals(request.ReceiptTypeId) && !x.IsDeleted).FirstOrDefault();
            if (existReceiptType == null)
                return BaseResponse<bool>.FailureResponse("Loại phiếu không tồn tại");

            string receiptCodePrefix = existReceiptType.SubCategoryName.Equals(RequestType.IMPORT.ToString()) ? "PNK" : "PXK";

            if (existRequest?.InventoryRequests?.FirstOrDefault()?.IsFulfilled == 1)
                return existReceiptType.SubCategoryName.Equals(RequestType.IMPORT.ToString())
                    ? BaseResponse<bool>.FailureResponse($"Phiếu yêu cầu nhập này đã được đạt số lượng yêu cầu")
                    : BaseResponse<bool>.FailureResponse($"Phiếu yêu cầu xuất này đã được đạt số lượng yêu cầu");

            if (receiptCodePrefix == "PXK")
            {
                foreach (var detail in request?.ReceiptDetails?.ToList())
                {
                    var stock = _unitOfWork.WareStockRepository.Get(x => x.ResourceId.Equals(detail.ResourceId) && x.WareId.Equals(request.WareFromId)).FirstOrDefault();
                    if (stock == null || stock.Quantity < detail.ActualQuantity)
                        return BaseResponse<bool>.FailureResponse("Không đủ hàng trong kho để xuất");
                }
            }

            var result = await _unitOfWork.ExecuteInTransactionAsync<BaseResponse<bool>>(async () =>
            {
                var inventoryReceipt = new InventoryReceipt
                {
                    InventoryRequestId = request.InventoryRequestId,
                    ReceiptTypeId = request.ReceiptTypeId,
                    ReceiptCodeNumber = $"{receiptCodePrefix}-{DateTime.Now.ToLocalTime().Ticks}",
                    BatchNumber = request.BatchNumber,
                    FarmId = existRequest?.FarmId,
                };

                _unitOfWork.InventoryReceiptRepository.Insert(inventoryReceipt);
                await _unitOfWork.SaveChangesAsync();

                foreach (var d in request.ReceiptDetails)
                {
                    var inventoryReceiptDetail = new InventoryReceiptDetail
                    {
                        ResourceId = d.ResourceId,
                        InventoryReceiptId = inventoryReceipt.InventoryReceiptId, 
                        ResourceSupplierId = null,
                        ActualQuantity = d.ActualQuantity,
                        ActualDate = DateTime.Now.ToLocalTime(),
                        Note = d.Note
                    };
                    _unitOfWork.InventoryReceiptDetailRepository.Insert(inventoryReceiptDetail);

                    var existResource = _unitOfWork.ResourceRepository.Get(filter: x => x.ResourceId.Equals(d.ResourceId)).FirstOrDefault();

                    var existResourceType = _unitOfWork.SubCategoryRepository.Get(filter: x => x.SubCategoryId.Equals(existResource.ResourceTypeId)).FirstOrDefault();

                    var typeName = existResourceType?.SubCategoryName;

                    var resourceId = typeName.Equals("food")
                        ? existResource?.FoodId
                        : typeName.Equals("equipment")
                            ? existResource?.EquipmentId
                            : typeName.Equals("equipment")
                                ? existResource?.MedicineId
                                    : typeName.Equals("breeding")
                                        ? existResource?.ChickenId
                                            : typeName.Equals("harvest_product")
                                                ? existResource?.HarvestProductId
                                                : null;

                    await _mediator.Publish(new StockUpdatedEvent(
                        resourceId ?? Guid.Empty,
                        receiptCodePrefix == "PNK" ? (int)d.ActualQuantity.Value : -(int)d.ActualQuantity.Value,
                        existResource?.UnitId,
                        typeName,
                        existResource?.PackageId,
                        existResource?.PackageSize,
                        receiptCodePrefix == "PNK" ? request.WareToId ?? Guid.Empty : request.WareFromId ?? Guid.Empty,
                        false
                    ));

                    var transaction = new WareTransaction
                    {
                        WareId = receiptCodePrefix == "PNK" ? request.WareToId : request.WareFromId,
                        ResourceId = d.ResourceId,
                        Quantity = receiptCodePrefix == "PNK" ? (int)d.ActualQuantity : (int)-d.ActualQuantity,
                        UnitId = existResource?.UnitId,
                        BatchNumber = request.BatchNumber,
                        TransactionType = existReceiptType.SubCategoryId,
                        Reason = d.Note,
                        TransactionDate = DateTime.Now.ToLocalTime(),
                        LocationFromId = receiptCodePrefix == "PNK" ? request.WareToId : request.WareFromId,
                        LocationToId = receiptCodePrefix == "PNK" ? request.WareToId : request.WareFromId
                    };
                    _unitOfWork.WareTransactionRepository.Insert(transaction);
                }

                await _unitOfWork.SaveChangesAsync();

                var inventoryRequestDetail = _unitOfWork.InventoryRequestDetailRepository.GetIncludeMultiLayer(x => x.InventoryRequestId == request.InventoryRequestId).ToList();

                var inventoryReceipts = _unitOfWork.InventoryReceiptRepository.GetIncludeMultiLayer(filter: x => x.InventoryRequestId.Equals(request.InventoryRequestId),
                    include: x => x
                    .Include(y => y.InventoryReceiptDetails)
                    ).ToList();

                var groupedReceiptDetails = inventoryReceipts
                    .SelectMany(r => r?.InventoryReceiptDetails)
                    .GroupBy(d => d.ResourceId)
                    .ToDictionary(g => g.Key, g => g.Sum(x => x.ActualQuantity ?? 0));

                int isAllEnough = inventoryRequestDetail.All(req =>
                {
                    var expected = req.ExpectedQuantity ?? 0;
                    var actual = groupedReceiptDetails.ContainsKey(req.ResourceId)
                        ? groupedReceiptDetails[req.ResourceId]
                        : 0;    
                    return actual >= expected;
                }) ? 1 : 0;

                existRequest.InventoryRequests.FirstOrDefault().IsFulfilled = isAllEnough;
                _unitOfWork.InventoryReceiptRepository.Update(inventoryReceipt);
                await _unitOfWork.SaveChangesAsync();
                return BaseResponse<bool>.SuccessResponse(true);
            });

            if (result.Data == false)
                return BaseResponse<bool>.FailureResponse("Tạo thất bại:" + result.Message);

            return BaseResponse<bool>.SuccessResponse($"Tạo phiếu {(receiptCodePrefix.Equals("PNK") ? "nhập" : "xuất")} thành công");
        }
        catch (Exception ex)
        {
            return BaseResponse<bool>.FailureResponse("Tạo thất bại: " + ex.Message);
        }
    }
}
