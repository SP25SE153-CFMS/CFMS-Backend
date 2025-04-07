using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.DTOs.Farm;
using CFMS.Application.DTOs.Task;
using CFMS.Application.DTOs.Warehouse;
using CFMS.Application.Features.WarehouseFeat.GetWares;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.TwiML.Voice;

namespace CFMS.Application.Features.WarehouseFeat.GetWaresByFarmId
{
    public class GetWaresByFarmIdQueryHandler : IRequestHandler<GetWaresByFarmIdQuery, BaseResponse<IEnumerable<WareResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetWaresByFarmIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<IEnumerable<WareResponse>>> Handle(GetWaresByFarmIdQuery request, CancellationToken cancellationToken)
        {
            var wares = _unitOfWork.WarehouseRepository.Get(filter: f => f.FarmId.Equals(request.FarmId) && f.IsDeleted == false);

            var resourceTypeIds = wares.Select(t => t.ResourceTypeId).Where(id => id.HasValue).Distinct().ToList();

            var subCategoryIds = resourceTypeIds.Where(id => id.HasValue).Distinct().ToList();

            var subCategories = _unitOfWork.SubCategoryRepository.Get(
                    filter: s => subCategoryIds.Contains(s.SubCategoryId)
                ).ToList();

            var wareDtos = wares.Select(t => new WareResponse
            {
                FarmId = t.FarmId,
                ResourceTypeId = t.ResourceTypeId,
                ResourceTypeName = subCategories.FirstOrDefault(s => s.SubCategoryId.Equals(t.ResourceTypeId)) is var subCategory && subCategory != null
                    ? subCategory.SubCategoryName.ToLower() switch
                    {
                        "food" => "Thực phẩm",
                        "equipment" => "Thiết bị",
                        "medicine" => "Dược phẩm",
                        _ => subCategory.SubCategoryName
                    }
                    : "Không xác định",
                WarehouseName = t.WarehouseName,
                MaxQuantity = t.MaxQuantity,
                MaxWeight = t.MaxWeight,
                CurrentQuantity = t.CurrentQuantity,
                CurrentWeight = t.CurrentWeight,
                Description = t.Description,
                Status = t.Status
            }).ToList();

            return BaseResponse<IEnumerable<WareResponse>>.SuccessResponse(_mapper.Map<IEnumerable<WareResponse>>(wareDtos));
        }
    }
}
