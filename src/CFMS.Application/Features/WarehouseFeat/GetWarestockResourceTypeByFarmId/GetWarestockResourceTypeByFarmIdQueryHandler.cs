using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.DTOs.Supplier;
using CFMS.Application.DTOs.WareStock;
using CFMS.Application.Features.WarehouseFeat.GetWareStocks;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            Expression<Func<Resource, bool>> filter;
            string resourceTypeName = "all";

            if (request.ResourceTypeName == "all")
            {
                var excludedSubCategoryIds = _unitOfWork.SubCategoryRepository
                    .Get(filter: f => f.SubCategoryName == "breeding" || f.SubCategoryName == "harvest_product")
                    .Select(f => f.SubCategoryId)
                    .ToList();

                filter = f => f.WareStocks.Any(t => t.Ware.Farm.FarmId == request.FarmId) && !f.IsDeleted && !excludedSubCategoryIds.Contains(f.ResourceTypeId ?? Guid.Empty);
            }
            else
            {
                var existResourceType = _unitOfWork.SubCategoryRepository.Get(filter: f => f.SubCategoryName.Equals(request.ResourceTypeName) && f.IsDeleted == false).FirstOrDefault();
                if (existResourceType == null)
                {
                    return BaseResponse<IEnumerable<object>>.FailureResponse("Loại hàng hoá không tồn tại");
                }

                resourceTypeName = request.ResourceTypeName;

                var subCategoryId = existResourceType?.SubCategoryId;
                filter = f => f.ResourceTypeId == subCategoryId &&
                              f.WareStocks.Any(t => t.Ware.Farm.FarmId == request.FarmId) &&
                              !f.IsDeleted;
            }

            var resources = _unitOfWork.ResourceRepository.GetIncludeMultiLayer(
                filter: filter,
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
                return BaseResponse<IEnumerable<object>>.SuccessResponse([], "Không có hàng hoá nào");
            }

            var wareStockFoodResponses = resources
                .SelectMany(r => r.ResourceSuppliers.DefaultIfEmpty(), (resource, supplier) =>
                {
                    var unit = _unitOfWork.SubCategoryRepository.Get(filter: f => f.SubCategoryId.Equals(resource.UnitId) && f.IsDeleted == false).FirstOrDefault();

                    var package = _unitOfWork.SubCategoryRepository.Get(filter: f => f.SubCategoryId.Equals(resource.PackageId) && f.IsDeleted == false).FirstOrDefault();

                    if (resource == null || unit == null || package == null)
                    {
                        return null;
                    }

                    var quantity = resource?.WareStocks.FirstOrDefault(x => x.ResourceId.Equals(resource.ResourceId))?.Quantity ?? 0;

                    var resourceSuppliers = _unitOfWork.ResourceSupplierRepository
                        .GetIncludeMultiLayer(
                            filter: f => f.ResourceId.Equals(resource.ResourceId)
                                      && f.IsDeleted == false,
                            include: x => x.Include(t => t.Supplier)
                        ).ToList();

                    var typeName = request.ResourceTypeName.Equals("all")
                        ? resource?.ResourceType?.SubCategoryName?.ToLower()
                        : resourceTypeName?.ToLower();

                    switch (typeName)
                    {
                        case "food":
                            return (WareStockResponseBase)new WareStockFoodResponse
                            {
                                ResourceId = resource?.ResourceId ?? Guid.Empty,
                                FoodId = resource?.FoodId ?? Guid.Empty,
                                FoodCode = resource?.Food?.FoodCode,
                                FoodName = resource?.Food?.FoodName,
                                Note = resource?.Food?.Note,
                                ProductionDate = resource?.Food?.ProductionDate,
                                ExpiryDate = resource?.Food?.ExpiryDate,
                                SpecQuantity = $"{(int)(quantity / resource?.PackageSize)} {package.SubCategoryName} (lẻ {quantity % resource?.PackageSize} {unit.SubCategoryName})",
                                UnitSpecification = $"{resource?.PackageSize} {unit.SubCategoryName}/{package.SubCategoryName}",
                                SupplierName = resourceSuppliers.Count > 0 ? $"Có {resourceSuppliers.Count} nhà cung cấp" : "Chưa có nhà cung cấp",
                                SuppliersName = resourceSuppliers
                                    .Where(rs => rs.Supplier != null)
                                    .GroupBy(rs => rs?.Supplier?.SupplierId)
                                    .Select(g =>
                                    {
                                        var first = g.First();
                                        var matchedResourceSupplier = g.FirstOrDefault(x =>
                                            x.ResourceId == resource?.ResourceId &&
                                            x.SupplierId == x.Supplier?.SupplierId);

                                        return new SupplierResponse
                                        {
                                            SupplierId = first.Supplier?.SupplierId,
                                            SupplierCode = first.Supplier?.SupplierCode,
                                            SupplierName = first.Supplier?.SupplierName,
                                            ResourceSupplierId = matchedResourceSupplier?.ResourceSupplierId
                                        };
                                    })
                                    .ToList()
                            };

                        case "equipment":
                            var existMaterial = _unitOfWork.SubCategoryRepository.Get(filter: f => f.SubCategoryId.Equals(resource.Equipment.MaterialId) && f.IsDeleted == false).FirstOrDefault();

                            return new WareStockEquipmentResponse
                            {
                                ResourceId = resource?.ResourceId ?? Guid.Empty,
                                EquipmentId = resource?.EquipmentId ?? Guid.Empty,
                                EquipmentCode = resource?.Equipment?.EquipmentCode,
                                EquipmentName = resource?.Equipment?.EquipmentName,
                                MaterialId = resource?.Equipment?.MaterialId,
                                Material = existMaterial?.SubCategoryName,
                                Usage = resource?.Equipment?.Usage,
                                Warranty = resource?.Equipment?.Warranty,
                                SizeUnitId = resource?.Equipment?.SizeUnitId,
                                Size = resource?.Equipment?.Size,
                                WeightUnitId = resource?.Equipment?.WeightUnitId,
                                Weight = resource?.Equipment?.Weight,
                                PurchaseDate = resource?.Equipment?.PurchaseDate,
                                SpecQuantity = $"{(int)(quantity / resource?.PackageSize)} {package.SubCategoryName} (lẻ {quantity % resource?.PackageSize} {unit.SubCategoryName})",
                                UnitSpecification = $"{resource?.PackageSize} {unit.SubCategoryName}/{package.SubCategoryName}",
                                SupplierName = resourceSuppliers.Count > 0 ? $"Có {resourceSuppliers.Count} nhà cung cấp" : "Chưa có nhà cung cấp",
                                SuppliersName = resourceSuppliers
                                    .Where(rs => rs.Supplier != null)
                                    .GroupBy(rs => rs?.Supplier?.SupplierId)
                                    .Select(g =>
                                    {
                                        var first = g.First();
                                        var matchedResourceSupplier = g.FirstOrDefault(x =>
                                            x.ResourceId == resource?.ResourceId &&
                                            x.SupplierId == x.Supplier?.SupplierId);

                                        return new SupplierResponse
                                        {
                                            SupplierId = first.Supplier?.SupplierId,
                                            SupplierCode = first.Supplier?.SupplierCode,
                                            SupplierName = first.Supplier?.SupplierName,
                                            ResourceSupplierId = matchedResourceSupplier?.ResourceSupplierId
                                        };
                                    })
                                    .ToList()
                            };

                        case "medicine":
                            var existDisease = _unitOfWork.SubCategoryRepository.Get(filter: f => f.SubCategoryId.Equals(resource.Medicine.DiseaseId) && f.IsDeleted == false).FirstOrDefault();

                            return new WareStockMedicineResponse
                            {
                                ResourceId = resource?.ResourceId ?? Guid.Empty,
                                MedicineId = resource?.MedicineId ?? Guid.Empty,
                                MedicineCode = resource?.Medicine?.MedicineCode,
                                MedicineName = resource?.Medicine?.MedicineName,
                                Usage = resource?.Medicine?.Usage,
                                DosageForm = resource?.Medicine?.DosageForm,
                                StorageCondition = resource?.Medicine?.StorageCondition,
                                DiseaseId = resource?.Medicine?.DiseaseId,
                                Disease = existDisease?.SubCategoryName,
                                ProductionDate = resource?.Medicine?.ProductionDate,
                                ExpiryDate = resource?.Medicine?.ExpiryDate,
                                SpecQuantity = $"{(int)(quantity / resource?.PackageSize)} {package.SubCategoryName} (lẻ {quantity % resource?.PackageSize} {unit.SubCategoryName})",
                                UnitSpecification = $"{resource?.PackageSize} {unit.SubCategoryName}/{package.SubCategoryName}",
                                SupplierName = resourceSuppliers.Count > 0 ? $"Có {resourceSuppliers.Count} nhà cung cấp" : "Chưa có nhà cung cấp",
                                SuppliersName = resourceSuppliers
                                    .Where(rs => rs.Supplier != null)
                                    .GroupBy(rs => rs?.Supplier?.SupplierId)
                                    .Select(g =>
                                    {
                                        var first = g.First();
                                        var matchedResourceSupplier = g.FirstOrDefault(x =>
                                            x.ResourceId == resource?.ResourceId &&
                                            x.SupplierId == x.Supplier?.SupplierId);

                                        return new SupplierResponse
                                        {
                                            SupplierId = first.Supplier?.SupplierId,
                                            SupplierCode = first.Supplier?.SupplierCode,
                                            SupplierName = first.Supplier?.SupplierName,
                                            ResourceSupplierId = matchedResourceSupplier?.ResourceSupplierId
                                        };
                                    })
                                    .ToList()
                            };

                        case "breeding":
                            var existChickenTypeName = _unitOfWork.SubCategoryRepository.Get(filter: f => f.SubCategoryId.Equals(resource.Chicken.ChickenTypeId) && f.IsDeleted == false).FirstOrDefault();

                            return new WareStockChickenBreedingResponse
                            {
                                ResourceId = resource?.ResourceId ?? Guid.Empty,
                                ChickenId = resource?.ChickenId ?? Guid.Empty,
                                ChickenCode = resource?.Chicken?.ChickenCode,
                                ChickenName = resource?.Chicken?.ChickenName,
                                Description = resource?.Chicken?.Description,
                                ChickenTypeName = existChickenTypeName?.SubCategoryName,
                                SpecQuantity = $"{(int)(quantity / resource?.PackageSize)} {package.SubCategoryName} (lẻ {quantity % resource?.PackageSize} {unit.SubCategoryName})",
                                UnitSpecification = $"{resource?.PackageSize} {unit.SubCategoryName}/{package.SubCategoryName}",
                                SupplierName = resourceSuppliers.Count > 0 ? $"Có {resourceSuppliers.Count} nhà cung cấp" : "Chưa có nhà cung cấp",
                                SuppliersName = resourceSuppliers
                                    .Where(rs => rs.Supplier != null)
                                    .GroupBy(rs => rs?.Supplier?.SupplierId)
                                    .Select(g =>
                                    {
                                        var first = g.First();
                                        var matchedResourceSupplier = g.FirstOrDefault(x =>
                                            x.ResourceId == resource?.ResourceId &&
                                            x.SupplierId == x.Supplier?.SupplierId);

                                        return new SupplierResponse
                                        {
                                            SupplierId = first.Supplier?.SupplierId,
                                            SupplierCode = first.Supplier?.SupplierCode,
                                            SupplierName = first.Supplier?.SupplierName,
                                            ResourceSupplierId = matchedResourceSupplier?.ResourceSupplierId
                                        };
                                    })
                                    .ToList()
                            };

                        case "harvest_product":
                            var existHarvestProductType = _unitOfWork.SubCategoryRepository.Get(filter: f => f.SubCategoryId.Equals(resource.HarvestProduct.HarvestProductTypeId) && f.IsDeleted == false).FirstOrDefault();

                            return new WareStockHavestProductResponse
                            {
                                ResourceId = resource?.ResourceId ?? Guid.Empty,
                                HarvestProductId = resource?.HarvestProductId ?? Guid.Empty,
                                HarvestProductCode = resource?.HarvestProduct?.HarvestProductName,
                                HarvestProductName = resource?.HarvestProduct?.HarvestProductName,
                                HarvestProductTypeId = existHarvestProductType?.SubCategoryId,
                                HarvestProductTypeName = existHarvestProductType?.SubCategoryName,
                                SpecQuantity = $"{(int)(quantity / resource?.PackageSize)} {package.SubCategoryName} (lẻ {quantity % resource?.PackageSize} {unit.SubCategoryName})",
                                UnitSpecification = $"{resource?.PackageSize} {unit.SubCategoryName}/{package.SubCategoryName}",
                                SupplierName = resourceSuppliers.Count > 0 ? $"Có {resourceSuppliers.Count} nhà cung cấp" : "Chưa có nhà cung cấp",
                                SuppliersName = resourceSuppliers
                                    .Where(rs => rs.Supplier != null)
                                    .GroupBy(rs => rs?.Supplier?.SupplierId)
                                    .Select(g =>
                                    {
                                        var first = g.First();
                                        var matchedResourceSupplier = g.FirstOrDefault(x =>
                                            x.ResourceId == resource?.ResourceId &&
                                            x.SupplierId == x.Supplier?.SupplierId);

                                        return new SupplierResponse
                                        {
                                            SupplierId = first.Supplier?.SupplierId,
                                            SupplierCode = first.Supplier?.SupplierCode,
                                            SupplierName = first.Supplier?.SupplierName,
                                            ResourceSupplierId = matchedResourceSupplier?.ResourceSupplierId
                                        };
                                    })
                                    .ToList()
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
