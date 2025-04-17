using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.Events;
using CFMS.Application.Features.FoodFeat.Create;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.HarvestProductFeat.Create
{
    public class CreateHarvestProductCommandHandler : IRequestHandler<CreateHarvestProductCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CreateHarvestProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<BaseResponse<bool>> Handle(CreateHarvestProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existHarvest = _unitOfWork.HarvestProductRepository.Get(filter: s => s.HarvestProductCode.Equals(request.HarvestProductCode) && s.HarvestProductName.Equals(request.HarvestProductName) && s.IsDeleted == false).FirstOrDefault();

                if (existHarvest == null)
                {
                    existHarvest = _mapper.Map<HarvestProduct>(request);
                    _unitOfWork.HarvestProductRepository.Insert(existHarvest);
                    var result = await _unitOfWork.SaveChangesAsync();
                }

                await _mediator.Publish(new StockUpdatedEvent
                     (
                        existHarvest.HarvestProductId,
                        0,
                        request.UnitId,
                        "harvest_product",
                        request.PackageId,
                        request.PackageSize,
                        request.WareId,
                        true
                    ));
                return BaseResponse<bool>.SuccessResponse("Thêm sản phẩm thu hoạch thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse("Thêm thất bại: " + ex.Message);
            }
        }
    }
}
