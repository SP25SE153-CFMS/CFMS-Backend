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
            var existBatch = _unitOfWork.ChickenBatchRepository.Get(
                filter: b => b.ChickenBatchId.Equals(request.ChickenBatchId) && b.IsDeleted == false,
                includeProperties: "ChickenDetails,QuantityLogs"
                ).FirstOrDefault();
            if (existBatch == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Lứa nuôi không tồn tại");
            }

            try
            {
                var totalChicken = existBatch.ChickenDetails.Sum(cd => cd.Quantity);
                //var totalLoggedQuantity = existBatch.QuantityLogs?.Sum(q => q.Quantity) ?? 0;
                var totalLogQuantityRequest = request.QuantityLogDetails.Sum(q => q.Quantity);

                if (totalLogQuantityRequest > totalChicken)
                {
                    return BaseResponse<bool>.FailureResponse(message: "Tổng số lượng log vượt quá số lượng hiện tại của lứa nuôi");
                }

                var quantityLog = new QuantityLog
                {
                    ChickenBatchId = request.ChickenBatchId,
                    Quantity = totalLogQuantityRequest,
                    LogDate = request.LogDate,
                    LogType = request.LogType,
                    Notes = request.Notes,
                    ImageUrl = request.ImageUrl,
                };

                foreach (var quantityLogDetail in request.QuantityLogDetails)
                {
                    var chickenDetail = existBatch.ChickenDetails
                        .FirstOrDefault(cd => cd.Gender == quantityLogDetail.Gender);

                    if (chickenDetail != null)
                    {
                        chickenDetail.Quantity -= quantityLogDetail.Quantity;

                        if (chickenDetail.Quantity < 0)
                        {
                            return BaseResponse<bool>.FailureResponse(message: "Số lượng gà theo giới tính không đủ");
                        }
                    }
                    else
                    {
                        return BaseResponse<bool>.FailureResponse(message: $"Không tìm thấy thông tin gà với giới tính {(quantityLogDetail.Gender == 0 ? "trống" : "mái")}");
                    }

                    quantityLog.QuantityLogDetails.Add(new QuantityLogDetail
                    {
                        Quantity = quantityLogDetail.Quantity,
                        Gender = quantityLogDetail.Gender,
                    });
                }

                existBatch.QuantityLogs.Add(quantityLog);

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
