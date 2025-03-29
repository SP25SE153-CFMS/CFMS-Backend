using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.Events;
using CFMS.Application.Features.SupplierFeat.Create;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.FoodFeat.Create
{
    public class CreateFoodCommandHandler : IRequestHandler<CreateFoodCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CreateFoodCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<BaseResponse<bool>> Handle(CreateFoodCommand request, CancellationToken cancellationToken)
        {
            var existFood = _unitOfWork.FoodRepository.Get(filter: s => s.FoodCode.Equals(request.FoodCode) || s.FoodName.Equals(request.FoodName) && s.IsDeleted == false).FirstOrDefault();
            if (existFood != null)
            {
                return BaseResponse<bool>.FailureResponse("Tên hoặc mã thực phẩm đã tồn tại");
            }

            var food = _mapper.Map<Food>(request);
            _unitOfWork.FoodRepository.Insert(food);
            var result = await _unitOfWork.SaveChangesAsync();

            //_mediator.Publish(new StockUpdatedEvent
            //{
            //    ResourceId = food.FoodId,
            //    ResourceTypeId = reqResourceType.Food,
            //    UnitId = request.UnitId,
            //    PackageId = request.PackageId,
            //    PackageSize = request.PackageSize,
            //    Quantity = 0,
            //    WareId = request.Wa
            //    IsCreatedCall = true
            //});

            return result > 0
                ? BaseResponse<bool>.SuccessResponse("Thêm thực phẩm thành công")
                : BaseResponse<bool>.FailureResponse("Thêm thất bại");
        }
    }
}
