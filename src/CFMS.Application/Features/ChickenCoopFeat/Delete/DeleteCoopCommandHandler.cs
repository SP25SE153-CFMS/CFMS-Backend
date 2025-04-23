using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.ChickenCoopFeat.Delete
{
    public class DeleteCoopCommandHandler : IRequestHandler<DeleteCoopCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCoopCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteCoopCommand request, CancellationToken cancellationToken)
        {
            var existCoop = _unitOfWork.ChickenCoopRepository.Get(filter: f => f.ChickenCoopId.Equals(request.Id) && f.IsDeleted == false).FirstOrDefault();
            if (existCoop == null)
            {
                return BaseResponse<bool>.SuccessResponse(message: "Chuồng gà không tồn tại");
            }

            try
            {
                _unitOfWork.ChickenCoopRepository.Delete(existCoop);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Xóa thành công");
                }
                return BaseResponse<bool>.SuccessResponse(message: "Xoá không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra:" + ex.Message);
            }
        }
    }
}
