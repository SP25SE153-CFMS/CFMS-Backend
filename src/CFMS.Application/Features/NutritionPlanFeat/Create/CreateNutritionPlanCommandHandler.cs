using AutoMapper;
using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.NutritionPlanFeat.Create
{
    public class CreateNutritionPlanCommandHandler : IRequestHandler<CreateNutritionPlanCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateNutritionPlanCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<bool>> Handle(CreateNutritionPlanCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existNutritionPlan = _unitOfWork.NutritionPlanRepository.Get(filter: p => p.Name.Equals(request.Name) && p.IsDeleted == false).FirstOrDefault();

                if (existNutritionPlan != null)
                {
                    return BaseResponse<bool>.FailureResponse("Tên chế độ dinh dưỡng đã tồn tại");
                }

                //var chickens = _unitOfWork.ChickenRepository.Get(filter: c => request.ChickenList.Contains(c.ChickenId)).ToList();
                //var missingIds = request.ChickenList.Except(chickens.Select(c => c.ChickenId)).ToList();

                //if (missingIds.Any())
                //{
                //    return BaseResponse<bool>.FailureResponse($"Không tồn tại loại gà nào");
                //}

                var nutritionPlan = new NutritionPlan
                {
                    Name = request.Name,
                    Description = request.Description
                };

                _unitOfWork.NutritionPlanRepository.Insert(nutritionPlan);

                await _unitOfWork.SaveChangesAsync();

                //var nutritionPlanChickens = chickens.Select(chicken => new ChickenNutrition
                //{
                //    NutritionPlanId = nutritionPlan.NutritionPlanId,
                //    ChickenId = chicken.ChickenId
                //}).ToList();

                //_unitOfWork.ChickenNutritionRepository.InsertRange(nutritionPlanChickens);

                existNutritionPlan = _unitOfWork.NutritionPlanRepository.Get(filter: p => p.Name.Equals(request.Name) & p.IsDeleted == false).FirstOrDefault();

                var nutritionPlanDetails = request.NutritionPlanDetails.Select(detail => new NutritionPlanDetail
                {
                    NutritionPlanId = existNutritionPlan.NutritionPlanId,
                    FoodId = detail.FoodId,
                    UnitId = detail.UnitId,
                    FoodWeight = detail.FoodWeight
                }).ToList();

                _unitOfWork.NutritionPlanDetailRepository.InsertRange(nutritionPlanDetails);

                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Tạo thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Tạo không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra:" + ex.Message);
            }
        }
    }
}
