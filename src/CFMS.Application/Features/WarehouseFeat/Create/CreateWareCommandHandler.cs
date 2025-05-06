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

namespace CFMS.Application.Features.WarehouseFeat.Create
{
    public class CreateWareCommandHandler : IRequestHandler<CreateWareCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateWareCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<bool>> Handle(CreateWareCommand request, CancellationToken cancellationToken)
        {
            var existWare = _unitOfWork.WarehouseRepository.Get(filter: s => s.FarmId.Equals(request.FarmId) && s.WarehouseName.Equals(request.WarehouseName) && s.IsDeleted == false).FirstOrDefault();
            if (existWare != null)
            {
                return BaseResponse<bool>.FailureResponse("Tên kho đã tồn tại");
            }

            var existFarm = _unitOfWork.FarmRepository.Get(filter: s => s.FarmId.Equals(request.FarmId) && s.IsDeleted == false).FirstOrDefault();
            if (existFarm == null)
            {
                return BaseResponse<bool>.FailureResponse("Trang trại không tồn tại");
            }

            var existResourceType = _unitOfWork.SubCategoryRepository.Get(filter: s => s.SubCategoryId.Equals(request.ResourceTypeId) && s.IsDeleted == false).FirstOrDefault();
            if (existResourceType == null)
            {
                return BaseResponse<bool>.FailureResponse("Loại hàng hoá không tồn tại");
            }

            var ware = _mapper.Map<Warehouse>(request);
            _unitOfWork.WarehouseRepository.Insert(ware);
            var result = await _unitOfWork.SaveChangesAsync();

            return result > 0
                ? BaseResponse<bool>.SuccessResponse("Thêm kho thành công")
                : BaseResponse<bool>.FailureResponse("Thêm thất bại");
        }
    }
}
