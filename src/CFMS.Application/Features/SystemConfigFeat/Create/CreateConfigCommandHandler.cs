using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.Features.FoodFeat.Create;
using CFMS.Domain.Entities;
using CFMS.Domain.Enums.Types;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.SystemConfigFeat.Create
{
    public class CreateConfigCommandHandler : IRequestHandler<CreateConfigCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateConfigCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<bool>> Handle(CreateConfigCommand request, CancellationToken cancellationToken)
        {
            var existConfig = _unitOfWork.SystemConfigRepository.Get(filter: s => s.SettingName.Equals(request.SettingName) && s.IsDeleted == false).FirstOrDefault();
            if (existConfig != null)
            {
                return BaseResponse<bool>.FailureResponse("Tên cấu hình đã tồn tại");
            }

            switch (request.EntityType)
            {
                case nameof(EntityType.COOP_TYPE):
                    var coop = _unitOfWork.ChickenCoopRepository
                        .Get(e => e.ChickenCoopId.Equals(request.EntityId) && !e.IsDeleted)
                        .FirstOrDefault();
                    if (coop == null)
                    {
                        return BaseResponse<bool>.FailureResponse("Chuồng gà không tồn tại");
                    }
                    break;

                case nameof(EntityType.WARE_TYPE):
                    var warehouse = _unitOfWork.WarehouseRepository
                        .Get(e => e.WareId.Equals(request.EntityId) && !e.IsDeleted)
                        .FirstOrDefault();
                    if (warehouse == null)
                    {
                        return BaseResponse<bool>.FailureResponse("Kho không tồn tại");
                    }
                    break;

                default:
                    return BaseResponse<bool>.FailureResponse("Đối tượng cấu hình không hợp lệ");
            }

            var config = _mapper.Map<SystemConfig>(request);
            _unitOfWork.SystemConfigRepository.Insert(config);
            var result = await _unitOfWork.SaveChangesAsync();

            return result > 0
                ? BaseResponse<bool>.SuccessResponse("Thêm cấu hình thành công")
                : BaseResponse<bool>.FailureResponse("Thêm thất bại");
        }
    }
}
