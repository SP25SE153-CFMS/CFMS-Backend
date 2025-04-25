using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.AddQuantityLog
{
    public class AddQuantityLogCommandHandler : IRequestHandler<AddQuantityLogCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddQuantityLogCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(AddQuantityLogCommand request, CancellationToken cancellationToken)
        {
            var existBatch = _unitOfWork.ChickenBatchRepository.Get(filter: b => b.ChickenBatchId.Equals(request.ChickenBatchId) && b.IsDeleted == false, includeProperties: "ChickenDetails,QuantityLogs").FirstOrDefault();
            if (existBatch == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Lứa nuôi không tồn tại");
            }

            try
            {
                var totalChicken = existBatch.ChickenDetails.Sum(cd => cd.Quantity);
                var totalLoggedQuantity = existBatch.QuantityLogs?.Sum(q => q.Quantity) ?? 0;

                if (request.Quantity > (totalChicken - totalLoggedQuantity))
                {
                    return BaseResponse<bool>.FailureResponse(message: "Tổng số lượng log vượt quá số lượng hiện tại của lứa nuôi");
                }

                existBatch.QuantityLogs.Add(new QuantityLog
                {
                    ChickenBatchId = request.ChickenBatchId,
                    Quantity = request.Quantity,
                    LogDate = request.LogDate,
                    LogType = request.LogType,
                    Notes = request.Notes,
                });

                _unitOfWork.ChickenBatchRepository.Update(existBatch);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Thêm thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Thêm không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra");
            }
        }
    }
}
