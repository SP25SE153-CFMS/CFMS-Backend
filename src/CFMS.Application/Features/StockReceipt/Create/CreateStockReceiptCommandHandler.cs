using CFMS.Application.Common;
using CFMS.Application.Events;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

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
                    StockReceiptCode = $"PNK-{DateTime.Now.ToLocalTime().AddHours(7).Ticks}"
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

                    stockReceipt.StockReceiptDetails.Add(new StockReceiptDetail
                    {
                        Quantity = stockReceiptDetail.Quantity,
                        ResourceId = stockReceiptDetail.ResourceId,
                        SupplierId = stockReceiptDetail.SupplierId,
                        ToWareId = stockReceiptDetail.ToWareId,
                        UnitId = stockReceiptDetail.UnitId,
                    });

                    await _mediator.Publish(new StockUpdatedEvent
                         (
                            existResource.ResourceId,
                            (int)stockReceiptDetail.Quantity,
                            existResource.UnitId,
                            existResource.ResourceType.SubCategoryName,
                            existResource.PackageId,
                            existResource.PackageSize,
                            stockReceiptDetail.ToWareId,
                            false,
                            null
                        ));
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
