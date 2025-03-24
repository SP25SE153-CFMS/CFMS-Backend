using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.ChickenFeat.Delete
{
    public class DeleteChickenCommandHandler : IRequestHandler<DeleteChickenCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteChickenCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteChickenCommand request, CancellationToken cancellationToken)
        {
            var existChicken = _unitOfWork.ChickenRepository.Get(filter: c => c.ChickenId.Equals(request.Id) && c.IsDeleted == false).FirstOrDefault();
            if (existChicken == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Gà không tồn tại");
            }

            try
            {
                _unitOfWork.ChickenRepository.Delete(existChicken);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Xóa thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Xóa không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra:" + ex.Message);
            }
        }
    }
}
