using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.NutritionPlanFeat.Update
{
    public class UpdateNutritionPlanCommandHandler : IRequestHandler<UpdateNutritionPlanCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateNutritionPlanCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(UpdateNutritionPlanCommand request, CancellationToken cancellationToken)
        {
            var existPlan = _unitOfWork.NutritionPlanRepository.Get(filter: p => p.NutritionPlanId.Equals(request.Id) && p.IsDeleted == false).FirstOrDefault();
            if (existPlan == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Chế độ dinh dưỡng không tồn tại");
            }

            try
            {
                existPlan.Description = request.Description;
                existPlan.Name = request.Name;
                //existPlan.Chickens = request.ChickensList;

                _unitOfWork.NutritionPlanRepository.Update(existPlan);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Cập nhật thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Cập nhật không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra");
            }
        }
    }
}
