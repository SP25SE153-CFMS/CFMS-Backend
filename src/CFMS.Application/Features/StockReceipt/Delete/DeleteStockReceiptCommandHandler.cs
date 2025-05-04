using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.StockReceipt.Delete
{
    public class DeleteStockReceiptCommandHandler : IRequestHandler<DeleteStockReceiptCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteStockReceiptCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteStockReceiptCommand request, CancellationToken cancellationToken)
        {
            var existStockReceipt = _unitOfWork.StockReceiptRepository.Get(filter: s => !s.IsDeleted && s.StockReceiptId.Equals(request.StockReceiptId)).FirstOrDefault();
            if (existStockReceipt == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Đơn nhập không tồn tại");
            }

            try
            {
                _unitOfWork.StockReceiptRepository.Delete(existStockReceipt);
                var result = await _unitOfWork.SaveChangesAsync();

                return result > 0 ?
                    BaseResponse<bool>.SuccessResponse(message: "Xóa thành công") :
                    BaseResponse<bool>.FailureResponse(message: "Xóa không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra: " + ex.Message);
            }
        }
    }
}
