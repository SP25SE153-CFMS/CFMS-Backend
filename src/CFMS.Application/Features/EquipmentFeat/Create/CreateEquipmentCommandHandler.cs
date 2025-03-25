using AutoMapper;
using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;

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
            var existEquip = _unitOfWork.EquipmentRepository.Get(filter: e => e.EquipmentCode.Equals(request.EquipmentCode) && e.IsDeleted == false).FirstOrDefault();
            if (existEquip == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Code đã tồn tại");
            }

            var existSizeUnit = _unitOfWork.SubCategoryRepository.Get(filter: u => u.SubCategoryId.Equals(request.SizeUnitId) && u.IsDeleted == false).FirstOrDefault();
            if (existSizeUnit == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "SizeUnit không tồn tại");

            }

            var existWeightUnit = _unitOfWork.SubCategoryRepository.Get(filter: u => u.SubCategoryId.Equals(request.WeightUnitId) && u.IsDeleted == false).FirstOrDefault();
            if (existWeightUnit == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "WeightUnit không tồn tại");
            }

            try
            {
                _unitOfWork.EquipmentRepository.Insert(_mapper.Map<Equipment>(request));
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Thêm thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Thêm không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: "Có lỗi xảy ra");
            }
        }
    }
}
