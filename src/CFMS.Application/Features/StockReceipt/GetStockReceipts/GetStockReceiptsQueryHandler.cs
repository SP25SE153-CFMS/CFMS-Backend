using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.StockReceipt.GetStockReceipts
{
    public class GetStockReceiptsQueryHandler : IRequestHandler<GetStockReceiptsQuery, BaseResponse<IEnumerable<Domain.Entities.StockReceipt>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetStockReceiptsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<IEnumerable<Domain.Entities.StockReceipt>>> Handle(GetStockReceiptsQuery request, CancellationToken cancellationToken)
        {
            var stockReceipts = _unitOfWork.StockReceiptRepository.Get(
                filter: s => !s.IsDeleted && s.FarmId.Equals(request.FarmId),
                includeProperties: "CreatedByUser,ReceiptType"
                );
            return BaseResponse<IEnumerable<Domain.Entities.StockReceipt>>.SuccessResponse(data: stockReceipts);
        }
    }
}
