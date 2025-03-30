using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.Events;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.EquipmentFeat.Create
{
    public class CreateEquipmentCommandHandler : IRequestHandler<CreateEquipmentCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CreateEquipmentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<BaseResponse<bool>> Handle(CreateEquipmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existSizeUnit = _unitOfWork.SubCategoryRepository.Get(filter: s => s.SubCategoryId.Equals(request.SizeUnitId) && s.IsDeleted == false).FirstOrDefault();
                if (existSizeUnit == null)
                {
                    return BaseResponse<bool>.FailureResponse("Đơn vị đo kích cỡ không tồn tại");
                }

                var existWeightUnit = _unitOfWork.SubCategoryRepository.Get(filter: s => s.SubCategoryId.Equals(request.WeightUnitId) && s.IsDeleted == false).FirstOrDefault();
                if (existWeightUnit == null)
                {
                    return BaseResponse<bool>.FailureResponse("Đơn vị đo khối lượng không tồn tại");
                }

                var equipmentType = _unitOfWork.SubCategoryRepository.Get(filter: x => x.SubCategoryName.Equals("equipment") && x.IsDeleted == false).FirstOrDefault();
                if (equipmentType == null)
                {
                    return BaseResponse<bool>.FailureResponse("Không tìm thấy loại trang thiết bị");
                }

                var existEquipment = _unitOfWork.EquipmentRepository.Get(filter: s => s.EquipmentCode.Equals(request.EquipmentCode) || s.EquipmentName.Equals(request.EquipmentName) && s.IsDeleted == false).FirstOrDefault();

                if (existEquipment == null)
                {
                    var equipment = _mapper.Map<Equipment>(request);
                    _unitOfWork.EquipmentRepository.Insert(equipment);
                    var result = await _unitOfWork.SaveChangesAsync();

                    await _mediator.Publish(new StockUpdatedEvent
                    (
                       existEquipment.EquipmentId,
                       0,
                       request.UnitId,
                       "equipment",
                       request.PackageId,
                       request.PackageSize,
                       request.WareId,
                       true
                   ));
                }
                return BaseResponse<bool>.SuccessResponse("Thêm trang thiết bị thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse("Thêm thất bại: " + ex.Message);
            }
        }
    }
}
