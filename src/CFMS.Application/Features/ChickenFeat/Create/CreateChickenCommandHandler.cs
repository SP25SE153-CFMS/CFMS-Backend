using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.Events;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.ChickenFeat.Create
{
    public class CreateChickenCommandHandler : IRequestHandler<CreateChickenCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CreateChickenCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<BaseResponse<bool>> Handle(CreateChickenCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //var existBatch = _unitOfWork.ChickenBatchRepository.Get(filter: b => b.ChickenBatchId.Equals(request.ChickenBatchId) && b.IsDeleted == false).FirstOrDefault();
                //if (existBatch == null)
                //{
                //    return BaseResponse<bool>.FailureResponse(message: "Lứa không tồn tại");
                //}

                var existChicken = _unitOfWork.ChickenRepository.Get(c => c.ChickenCode.Equals(request.ChickenCode) && c.ChickenName.Equals(request.ChickenName) && c.IsDeleted == false).FirstOrDefault();
                if (existChicken == null)
                {
                    existChicken = new Chicken
                    {
                        ChickenCode = request.ChickenCode,
                        ChickenName = request.ChickenName,
                        Description = request.Description,
                        Status = request.Status,
                        ChickenTypeId = request.ChickenTypeId,
                    };

                    _unitOfWork.ChickenRepository.Insert(existChicken);
                    var result = await _unitOfWork.SaveChangesAsync();
                }

                await _mediator.Publish(new StockUpdatedEvent
                     (
                        existChicken.ChickenId,
                        0,
                        request.UnitId,
                        "breeding",
                        request.PackageId,
                        request.PackageSize,
                        request.WareId,
                        true
                    ));

                //await _unitOfWork.SaveChangesAsync();

                //existChicken = _unitOfWork.ChickenRepository.Get(filter: p => p.ChickenCode.Equals(request.ChickenCode) && p.IsDeleted == false).FirstOrDefault();

                //var chickenDetails = request.ChickenDetails.Select(detail => new ChickenDetail
                //{
                //    ChickenId = existChicken.ChickenId,
                //    Weight = detail.Weight,
                //    Quantity = detail.Quantity,
                //    Gender = detail.Gender
                //}).ToList() ?? new List<ChickenDetail>();

                //_unitOfWork.ChickenDetailRepository.InsertRange(chickenDetails);
                return BaseResponse<bool>.SuccessResponse(message: "Thêm thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra:" + ex.Message);
            }
        }
    }
}
