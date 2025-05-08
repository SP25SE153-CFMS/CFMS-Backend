using CFMS.Application.Common;
using CFMS.Application.Events;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace CFMS.Application.Features.StockReceipt.Create
{
    public class CreateStockReceiptCommandHandler : IRequestHandler<CreateStockReceiptCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public CreateStockReceiptCommandHandler(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<BaseResponse<bool>> Handle(CreateStockReceiptCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var stockReceipt = new Domain.Entities.StockReceipt
                {
                    ReceiptTypeId = request.ReceiptTypeId,
                    FarmId = request.FarmId,
                    StockReceiptCode = $"DNK-{DateTime.UtcNow.ToLocalTime().AddHours(7).Ticks}"
                };

                foreach (var stockReceiptDetail in request.StockReceiptDetails)
                {
                    var existResource = _unitOfWork.ResourceRepository.Get(
                        filter: re => re.ResourceId.Equals(stockReceiptDetail.ResourceId) && !re.IsDeleted,
                        includeProperties: "ResourceType,Unit,Package"
                        ).FirstOrDefault();
                    if (existResource == null)
                    {
                        return BaseResponse<bool>.FailureResponse(message: "Resource không tồn tại");
                    }

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

                    stockReceipt.StockReceiptDetails.Add(new StockReceiptDetail
                    {
                        Quantity = stockReceiptDetail.Quantity,
                        ResourceId = stockReceiptDetail.ResourceId,
                        SupplierId = stockReceiptDetail.SupplierId,
                        ToWarehouseId = stockReceiptDetail.ToWareId,
                        UnitId = stockReceiptDetail.UnitId,
                    });

                    await _mediator.Publish(new StockUpdatedEvent
                         (
                            resourceId ?? Guid.Empty,
                            (int)stockReceiptDetail.Quantity,
                            existResource.UnitId,
                            existResource.ResourceType.SubCategoryName,
                            existResource.PackageId,
                            existResource.PackageSize,
                            stockReceiptDetail.ToWareId,
                            false,
                            stockReceiptDetail.SupplierId
                        ));

                    var transaction = new WareTransaction
                    {
                        WareId = stockReceiptDetail.ToWareId,
                        ResourceId = existResource.ResourceId,
                        Quantity = (int)stockReceiptDetail.Quantity,
                        UnitId = existResource?.UnitId,
                        BatchNumber = 1,
                        TransactionType = Guid.Parse("2d004c3f-f081-4986-b88a-644a43200f4b"),
                        TransactionDate = DateTime.UtcNow.ToLocalTime().AddHours(7),
                        LocationToId = stockReceiptDetail.ToWareId
                    };
                    _unitOfWork.WareTransactionRepository.Insert(transaction);
                }

                _unitOfWork.StockReceiptRepository.Insert(stockReceipt);
                var result = await _unitOfWork.SaveChangesAsync();

                return result > 0 ?
                    BaseResponse<bool>.SuccessResponse(message: "Tạo thành công") :
                    BaseResponse<bool>.FailureResponse(message: "Tạo ko thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra: " + ex.Message);
            }
        }
    }
}
