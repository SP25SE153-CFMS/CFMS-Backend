using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.StockReceipt.GetStockReceipt
{
    public class GetStockReceiptQueryHandler : IRequestHandler<GetStockReceiptQuery, BaseResponse<Domain.Entities.StockReceipt>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetStockReceiptQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<Domain.Entities.StockReceipt>> Handle(GetStockReceiptQuery request, CancellationToken cancellationToken)
        {
            var existStockReceipt = _unitOfWork.StockReceiptRepository.Get(filter: s => s.StockReceiptId.Equals(request.StockReceiptId) && !s.IsDeleted).FirstOrDefault();
            if (existStockReceipt == null)
            {
                return BaseResponse<Domain.Entities.StockReceipt>.FailureResponse(message: "Đơn nhập không tồn tại");
            }
            return BaseResponse<Domain.Entities.StockReceipt>.SuccessResponse(data: existStockReceipt);
        }
    }
}
