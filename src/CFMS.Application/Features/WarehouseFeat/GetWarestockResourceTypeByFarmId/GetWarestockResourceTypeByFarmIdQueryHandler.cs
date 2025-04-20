using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.DTOs.WareStock;
using CFMS.Application.Features.WarehouseFeat.GetWareStocks;
using CFMS.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.WarehouseFeat.GetWarestockResourceTypeByFarmId
{
    public class GetWarestockResourceTypeByFarmIdQueryHandler : IRequestHandler<GetWarestockResourceTypeByFarmIdQuery, BaseResponse<IEnumerable<object>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetWarestockResourceTypeByFarmIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<IEnumerable<object>>> Handle(GetWarestockResourceTypeByFarmIdQuery request, CancellationToken cancellationToken)
        {
            var existResourceType = _unitOfWork.SubCategoryRepository.Get(filter: f => f.SubCategoryName.Equals(request.ResourceTypeName) && f.IsDeleted == false).FirstOrDefault();
            if (!request.ResourceTypeName.Equals("all") && existResourceType == null)
            {
                return BaseResponse<IEnumerable<object>>.FailureResponse("Loại hàng hoá không tồn tại");
            }

            var resources = _unitOfWork.ResourceRepository.GetIncludeMultiLayer(
                filter: f => request.ResourceTypeName.Equals("all") || f.ResourceTypeId.Equals(existResourceType.SubCategoryId) && f.WareStocks.Any(t => t.Ware.Farm.FarmId.Equals(request.FarmId)) && f.IsDeleted == false,
                include: r => r
                .Include(x => x.ResourceType)
                .Include(x => x.ResourceSuppliers)
                .Include(x => x.Food)
                .Include(x => x.Equipment)
                .Include(x => x.Medicine)
                .Include(x => x.HarvestProduct)
                .Include(x => x.Chicken)
                .Include(x => x.WareStocks)
                    .ThenInclude(f => f.Ware)
                        .ThenInclude(f => f.Farm)
            ).ToList();

            if (resources.Count == 0)
            {
                return BaseResponse<IEnumerable<object>>.FailureResponse("Không có hàng hoá nào");
            }

            var wareStockFoodResponses = resources
                .SelectMany(r => r.ResourceSuppliers.DefaultIfEmpty(), (resource, supplier) =>
                {
                    if (resource == null)
                    {
                        return null;
                    }

                    var typeName = request.ResourceTypeName.Equals("all")
                        ? resource?.ResourceType?.SubCategoryName?.ToLower()
                        : existResourceType?.SubCategoryName?.ToLower();

                    switch (typeName)
                    {
                        case "food":
                            return (WareStockResponseBase)new WareStockFoodResponse
                            {
                                FoodId = resource?.FoodId ?? Guid.Empty,
                                FoodCode = resource?.Food?.FoodCode,
                                FoodName = resource?.Food?.FoodName,
                            };

                        case "equipment":
                            return new WareStockEquipmentResponse
                            {
                                EquipmentId = resource?.EquipmentId ?? Guid.Empty,
                                EquipmentCode = resource?.Equipment?.EquipmentCode,
                                EquipmentName = resource?.Equipment?.EquipmentName,
                            };

                        case "medicine":
                            return new WareStockMedicineResponse
                            {
                                MedicineId = resource?.MedicineId ?? Guid.Empty,
                                MedicineCode = resource?.Medicine?.MedicineCode,
                                MedicineName = resource?.Medicine?.MedicineName,
                            };

                        case "breeding":
                            return new WareStockChickenBreedingResponse
                            {
                                ChickenId = resource?.ChickenId ?? Guid.Empty,
                                ChickenCode = resource?.Chicken?.ChickenCode,
                                ChickenName = resource?.Chicken?.ChickenName,
                            };

                        case "harvest_product":
                            var existHarvestProductType = _unitOfWork.SubCategoryRepository.Get(filter: f => f.SubCategoryId.Equals(resource.HarvestProduct.HarvestProductTypeId) && f.IsDeleted == false).FirstOrDefault();

                            return new WareStockHavestProductResponse
                            {
                                HarvestProductId = resource?.HarvestProductId ?? Guid.Empty,
                                HarvestProductCode = resource?.HarvestProduct?.HarvestProductCode,
                                HarvestProductName = resource?.HarvestProduct?.HarvestProductName,
                                HarvestProductTypeId = existHarvestProductType?.SubCategoryId,
                                HarvestProductTypeName = existHarvestProductType?.SubCategoryName,
                            };

                        default:
                            return null;
                    }
                })
                .Where(x => x != null)
                .DistinctBy(x =>
                {
                    return x switch
                    {
                        WareStockFoodResponse food => food.FoodId,
                        WareStockEquipmentResponse equip => equip.EquipmentId,
                        WareStockMedicineResponse med => med.MedicineId,
                        WareStockChickenBreedingResponse breed => breed.ChickenId,
                        WareStockHavestProductResponse harvest => harvest.HarvestProductId,
                        _ => Guid.NewGuid()
                    };
                })
                .ToList();

            return BaseResponse<IEnumerable<object>>.SuccessResponse(wareStockFoodResponses);
        }
    }
}
