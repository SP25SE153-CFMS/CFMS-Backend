using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.ChickenFeat.UpdateChickenDetail
{
    public class UpdateChickenDetailCommandHandler : IRequestHandler<UpdateChickenDetailCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateChickenDetailCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(UpdateChickenDetailCommand request, CancellationToken cancellationToken)
        {
            var existChicken = _unitOfWork.ChickenRepository.Get(filter: c => c.ChickenId.Equals(request.ChickenId) && c.IsDeleted == false).FirstOrDefault();
            if (existChicken == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Gà không tồn tại");
            }

            var existDetail = _unitOfWork.ChickenDetailRepository.Get(filter: d => d.ChickenDetailId.Equals(request.ChickenDetailId) && d.IsDeleted == false).FirstOrDefault();
            if (existDetail == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Detail không tồn tại");
            }

            try
            {
                existDetail.Weight = request.Weight;
                existDetail.Gender = request.Gender;
                existDetail.Quantity = request.Quantity;

                _unitOfWork.ChickenDetailRepository.Update(existDetail);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Cập nhật thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Cập nhật ko thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra");
            }
        }
    }
}
