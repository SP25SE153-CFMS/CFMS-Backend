using AutoMapper;
using CFMS.Application.Common;
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

        public CreateEquipmentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<bool>> Handle(CreateEquipmentCommand request, CancellationToken cancellationToken)
        {
            var existEquipment = _unitOfWork.EquipmentRepository.Get(filter: s => s.EquipmentCode.Equals(request.EquipmentCode) || s.EquipmentName.Equals(request.EquipmentName)).FirstOrDefault();
            if (existEquipment != null)
            {
                return BaseResponse<bool>.FailureResponse("Tên hoặc mã trang thiết bị đã tồn tại");
            }

            var equipment = _mapper.Map<Equipment>(request);
            _unitOfWork.EquipmentRepository.Insert(equipment);
            var result = await _unitOfWork.SaveChangesAsync();

            return result > 0
                ? BaseResponse<bool>.SuccessResponse("Thêm trang thiết bị thành công")
                : BaseResponse<bool>.FailureResponse("Thêm thất bại");
        }
    }
}
