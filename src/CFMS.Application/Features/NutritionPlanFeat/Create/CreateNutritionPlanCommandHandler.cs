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
                var existNutritionPlan = _unitOfWork.NutritionPlanRepository.Get(filter: p => p.Name.Equals(request.Name) && p.IsDeleted == false && p.FarmId.Equals(request.FarmId)).FirstOrDefault();
                if (existNutritionPlan != null)
                {
                    return BaseResponse<bool>.FailureResponse("Tên chế độ dinh dưỡng đã tồn tại");
                }

                var nutritionPlan = new NutritionPlan
                {
                    Name = request.Name,
                    Description = request.Description,
                    FarmId = request.FarmId,
                };

                foreach (var nutritionPlanDetail in request.NutritionPlanDetails)
                {
                    nutritionPlan.NutritionPlanDetails.Add(new NutritionPlanDetail
                    {
                        FoodId = nutritionPlanDetail.FoodId,
                        UnitId = nutritionPlanDetail.UnitId,
                        FoodWeight = nutritionPlanDetail.FoodWeight,
                    });
                }

                foreach (var feedSession in request.FeedSessions)
                {
                    nutritionPlan.FeedSessions.Add(new FeedSession
                    {
                        FeedAmount = feedSession.FeedAmount,
                        UnitId = feedSession.UnitId,
                        StartTime = feedSession.StartTime,
                        EndTime = feedSession.EndTime,
                        Note = feedSession.Note,
                    });
                }

                _unitOfWork.NutritionPlanRepository.Insert(nutritionPlan);
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
