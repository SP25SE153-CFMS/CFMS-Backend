using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.Update
{
    public class UpdateChickenBatchCommandHandler : IRequestHandler<UpdateChickenBatchCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUtilityService _utilityService;

        public UpdateChickenBatchCommandHandler(IUnitOfWork unitOfWork, IUtilityService utilityService)
        {
            _unitOfWork = unitOfWork;
            _utilityService = utilityService;
        }

        public async Task<BaseResponse<bool>> Handle(UpdateChickenBatchCommand request, CancellationToken cancellationToken)
        {
            var existBatch = _unitOfWork.ChickenBatchRepository.Get(filter: b => b.ChickenBatchId.Equals(request.Id) && b.IsDeleted == false).FirstOrDefault();
            if (existBatch == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Lứa không tồn tại");
            }

            try
            {
                existBatch.ChickenBatchName = request.ChickenBatchName;
                existBatch.StartDate = request.StartDate;
                existBatch.EndDate = request.EndDate;
                existBatch.Status = request.Status;

                _unitOfWork.ChickenBatchRepository.Update(existBatch);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Cập nhật thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Cập nhật không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra:" + ex.Message);
            }
        }
    }
}
