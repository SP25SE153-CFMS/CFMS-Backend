using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.Features.FoodFeat.Create;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.ResourceFeat.Create
{
    public class CreateResourceCommandHandler : IRequestHandler<CreateResourceCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateResourceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<bool>> Handle(CreateResourceCommand request, CancellationToken cancellationToken)
        {
            var existResourceType = _unitOfWork.SubCategoryRepository.Get(filter: s => s.SubCategoryId.Equals(request.ResourceTypeId) && s.IsDeleted == false).FirstOrDefault();

            if (existResourceType != null)
            {
                return BaseResponse<bool>.FailureResponse("Loại hàng hoá không tồn tại");
            }

            var existUnit = _unitOfWork.SubCategoryRepository.Get(filter: s => s.SubCategoryId.Equals(request.UnitId) && s.IsDeleted == false).FirstOrDefault();

            if (existUnit != null)
            {
                return BaseResponse<bool>.FailureResponse("Đơn vị đo không tồn tại");
            }

            var existPackage = _unitOfWork.SubCategoryRepository.Get(filter: s => s.SubCategoryId.Equals(request.PackageId) && s.IsDeleted == false).FirstOrDefault();

            if (existPackage != null)
            {
                return BaseResponse<bool>.FailureResponse("Loại đóng gói không tồn tại");
            }

            if (request.FoodId == null && request.EquipmentId == null && request.MedicineId == null)
            {
                return BaseResponse<bool>.FailureResponse("Hàng hoá đính kèm không tồn tại");
            }

            var resource = _mapper.Map<Resource>(request);
            _unitOfWork.ResourceRepository.Insert(resource);
            var result = await _unitOfWork.SaveChangesAsync();

            return result > 0
                ? BaseResponse<bool>.SuccessResponse("Thêm hàng hoá thành công")
                : BaseResponse<bool>.FailureResponse("Thêm thất bại");
        }
    }
}
