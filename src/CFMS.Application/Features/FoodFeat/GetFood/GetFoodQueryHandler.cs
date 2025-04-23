using CFMS.Application.Common;
using CFMS.Application.Features.ResourceFeat.GetResource;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.FoodFeat.GetFood
{
    public class GetFoodQueryHandler : IRequestHandler<GetFoodQuery, BaseResponse<Food>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetFoodQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<Food>> Handle(GetFoodQuery request, CancellationToken cancellationToken)
        {
            var existFood = _unitOfWork.FoodRepository.Get(filter: f => f.FoodId.Equals(request.Id) && f.IsDeleted == false).FirstOrDefault();
            if (existFood == null)
            {
                return BaseResponse<Food>.SuccessResponse(message: "Thức ăn không tồn tại");
            }

            return BaseResponse<Food>.SuccessResponse(data: existFood);
        }
    }
}
