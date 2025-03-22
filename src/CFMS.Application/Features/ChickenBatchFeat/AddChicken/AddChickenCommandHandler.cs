using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.ChickenBatchFeat.AddChicken
{
    public class AddChickenCommandHandler : IRequestHandler<AddChickenCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddChickenCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(AddChickenCommand request, CancellationToken cancellationToken)
        {
            var existBatch = _unitOfWork.ChickenBatchRepository.Get(filter: b => b.ChickenBatchId.Equals(request.ChickenBatchId) && b.IsDeleted == false).FirstOrDefault();
            if (existBatch == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Lứa không tồn tại");
            }

            var existChicken = _unitOfWork.ChickenRepository.Get(filter: c => c.ChickenId.Equals(request.ChickenId) && c.IsDeleted == false).FirstOrDefault();
            if (existChicken == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Gà không tồn tại");
            }

            try
            {
                //thêm quantity log
                existBatch.Chickens.Add(existChicken);

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
