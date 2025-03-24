using CFMS.Application.Common;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.NutritionPlanFeat.Delete
{
    public class DeleteNutritionPlanCommandHandler : IRequestHandler<DeleteNutritionPlanCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteNutritionPlanCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteNutritionPlanCommand request, CancellationToken cancellationToken)
        {
            var existPlan = _unitOfWork.NutritionPlanRepository.Get(filter: p => p.NutritionPlanId.Equals(request.Id) && p.IsDeleted == false).FirstOrDefault();
            if (existPlan == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Chế độ dinh dưỡng không tồn tại");
            }

            try
            {
                _unitOfWork.NutritionPlanRepository.Delete(existPlan);
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
