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
using Twilio.Base;

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
            Expression<Func<Domain.Entities.Resource, bool>> filter;
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

            var responses = resources.SelectMany(resource =>
            {
                var unit = _unitOfWork.SubCategoryRepository.Get(filter: f => f.SubCategoryId.Equals(resource.UnitId) && f.IsDeleted == false).FirstOrDefault();

                var package = _unitOfWork.SubCategoryRepository.Get(filter: f => f.SubCategoryId.Equals(resource.PackageId) && f.IsDeleted == false).FirstOrDefault();

                if (resource == null || unit == null || package == null)
                {
                    return null;
                }

                var resourceSuppliers = _unitOfWork.ResourceSupplierRepository
                    .GetIncludeMultiLayer(
                        filter: f => f.ResourceId.Equals(resource.ResourceId) && f.IsDeleted == false,
                        include: x => x
                            .Include(t => t.Supplier)
                            .Include(t => t.Resource)
                                .ThenInclude(t => t.Food)
                    ).ToList();

                var typeName = request.ResourceTypeName.Equals("all")
                    ? resource?.ResourceType?.SubCategoryName?.ToLower()
                    : resourceTypeName?.ToLower();

                if (!resourceSuppliers.Any())
                {
                    var quantity = 0;

                    if (typeName == "food" && resource.WareStocks.Any(x => x.SupplierId == null))
                    {
                        var defaultResponse = (WareStockResponseBase)new WareStockFoodResponse
                        {
                            ResourceId = resource.ResourceId,
                            FoodId = resource.FoodId ?? Guid.Empty,
                            FoodCode = resource.Food?.FoodCode,
                            FoodName = resource.Food?.FoodName,
                            Note = resource.Food?.Note,
                            ProductionDate = resource.Food?.ProductionDate,
                            ExpiryDate = resource.Food?.ExpiryDate,
                            SpecQuantity = (resource?.PackageSize > 0)
                                                ? $"{(int)(quantity / resource.PackageSize)} {package.SubCategoryName}" +
                            $"{(quantity % resource.PackageSize > 0 ? $" (lẻ {quantity % resource.PackageSize} {unit.SubCategoryName})" : "")}"
                                                : $"{(int)quantity} {unit.SubCategoryName}",
                            UnitSpecification = $"{resource.PackageSize} {unit.SubCategoryName}/{package.SubCategoryName}",
                            CurrentSupplierId = null,
                            CurrentSupplierName = "Không rõ nguồn gốc",
                            SupplierName = resourceSuppliers.Count > 0 ? $"Có {resourceSuppliers.Count} nhà cung cấp" : "Không rõ nguồn gốc",
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

                        return new List<WareStockResponseBase> { defaultResponse };
                    }

                    if (typeName == "equipment" && resource.WareStocks.Any(x => x.SupplierId == null))
                    {
                        var existMaterial = _unitOfWork.SubCategoryRepository.Get(filter: f => f.SubCategoryId.Equals(resource.Equipment.MaterialId) && f.IsDeleted == false).FirstOrDefault();

                        var defaultResponse = new WareStockEquipmentResponse
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
                            SpecQuantity = (resource?.PackageSize > 0)
                            ? $"{(int)(quantity / resource.PackageSize)} {package.SubCategoryName}" +
                            $"{(quantity % resource.PackageSize > 0 ? $" (lẻ {quantity % resource.PackageSize} {unit.SubCategoryName})" : "")}"
                                                : $"{(int)quantity} {unit.SubCategoryName}",
                            UnitSpecification = $"{resource.PackageSize} {unit.SubCategoryName}/{package.SubCategoryName}",
                            CurrentSupplierId = null,
                            CurrentSupplierName = "Không rõ nguồn gốc",
                            SupplierName = resourceSuppliers.Count > 0 ? $"Có {resourceSuppliers.Count} nhà cung cấp" : "Không rõ nguồn gốc",
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

                        return new List<WareStockEquipmentResponse> { defaultResponse };
                    }

                    if (typeName == "medicine" && resource.WareStocks.Any(x => x.SupplierId == null))
                    {
                        var existDisease = _unitOfWork.SubCategoryRepository.Get(filter: f => f.SubCategoryId.Equals(resource.Medicine.DiseaseId) && f.IsDeleted == false).FirstOrDefault();

                        var defaultResponse = new WareStockMedicineResponse
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
                            SpecQuantity = (resource?.PackageSize > 0)
                                                ? $"{(int)(quantity / resource.PackageSize)} {package.SubCategoryName}" +
                                                  $"{(quantity % resource.PackageSize > 0 ? $" (lẻ {quantity % resource.PackageSize} {unit.SubCategoryName})" : "")}"
                                                : $"{(int)quantity} {unit.SubCategoryName}",
                            UnitSpecification = $"{resource.PackageSize} {unit.SubCategoryName}/{package.SubCategoryName}",
                            CurrentSupplierId = null,
                            CurrentSupplierName = "Không rõ nguồn gốc",
                            SupplierName = resourceSuppliers.Count > 0 ? $"Có {resourceSuppliers.Count} nhà cung cấp" : "Không rõ nguồn gốc",
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

                        return new List<WareStockMedicineResponse> { defaultResponse };
                    }

                    if (typeName == "breeding" && resource.WareStocks.Any(x => x.SupplierId == null))
                    {
                        var existChickenTypeName = _unitOfWork.SubCategoryRepository.Get(filter: f => f.SubCategoryId.Equals(resource.Chicken.ChickenTypeId) && f.IsDeleted == false).FirstOrDefault();

                        var defaultResponse = new WareStockChickenBreedingResponse
                        {
                            ResourceId = resource?.ResourceId ?? Guid.Empty,
                            ChickenId = resource?.ChickenId ?? Guid.Empty,
                            ChickenCode = resource?.Chicken?.ChickenCode,
                            ChickenName = resource?.Chicken?.ChickenName,
                            Description = resource?.Chicken?.Description,
                            ChickenTypeName = existChickenTypeName?.SubCategoryName,
                            SpecQuantity = (resource?.PackageSize > 0)
                                                ? $"{(int)(quantity / resource.PackageSize)} {package.SubCategoryName}" +
                                                  $"{(quantity % resource.PackageSize > 0 ? $" (lẻ {quantity % resource.PackageSize} {unit.SubCategoryName})" : "")}"
                                                : $"{(int)quantity} {unit.SubCategoryName}",
                            UnitSpecification = $"{resource.PackageSize} {unit.SubCategoryName}/{package.SubCategoryName}",
                            CurrentSupplierId = null,
                            CurrentSupplierName = "Không rõ nguồn gốc",
                            SupplierName = resourceSuppliers.Count > 0 ? $"Có {resourceSuppliers.Count} nhà cung cấp" : "Không rõ nguồn gốc",
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

                        return new List<WareStockChickenBreedingResponse> { defaultResponse };
                    }

                    if (typeName == "harvest_product" && resource.WareStocks.Any(x => x.SupplierId == null))
                    {
                        var existHarvestProductType = _unitOfWork.SubCategoryRepository.Get(filter: f => f.SubCategoryId.Equals(resource.HarvestProduct.HarvestProductTypeId) && f.IsDeleted == false).FirstOrDefault();

                        var defaultResponse = new WareStockHavestProductResponse
                        {
                            ResourceId = resource?.ResourceId ?? Guid.Empty,
                            HarvestProductId = resource?.HarvestProductId ?? Guid.Empty,
                            HarvestProductCode = resource?.HarvestProduct?.HarvestProductCode,
                            HarvestProductName = resource?.HarvestProduct?.HarvestProductName,
                            HarvestProductTypeId = existHarvestProductType?.SubCategoryId,
                            HarvestProductTypeName = existHarvestProductType?.SubCategoryName,
                            SpecQuantity = (resource?.PackageSize > 0)
                                                ? $"{(int)(quantity / resource.PackageSize)} {package.SubCategoryName}" +
                                                  $"{(quantity % resource.PackageSize > 0 ? $" (lẻ {quantity % resource.PackageSize} {unit.SubCategoryName})" : "")}"
                                                : $"{(int)quantity} {unit.SubCategoryName}",
                            UnitSpecification = $"{resource.PackageSize} {unit.SubCategoryName}/{package.SubCategoryName}",
                            CurrentSupplierId = null,
                            CurrentSupplierName = "Không rõ nguồn gốc",
                            SupplierName = resourceSuppliers.Count > 0 ? $"Có {resourceSuppliers.Count} nhà cung cấp" : "Không rõ nguồn gốc",
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

                        return new List<WareStockHavestProductResponse> { defaultResponse };
                    }
                }

                var result = resourceSuppliers
                    .Where(rs => rs.Supplier != null)
                    .Select(rs =>
                    {
                        var quantity = 0;

                        if (typeName == "food" && resource.WareStocks.Any(x => x.SupplierId == rs.Supplier.SupplierId))
                        {
                            return (WareStockResponseBase)new WareStockFoodResponse
                            {
                                ResourceId = resource.ResourceId,
                                FoodId = resource.FoodId ?? Guid.Empty,
                                FoodCode = resource.Food?.FoodCode,
                                FoodName = resource.Food?.FoodName,
                                Note = resource.Food?.Note,
                                ProductionDate = resource.Food?.ProductionDate,
                                ExpiryDate = resource.Food?.ExpiryDate,
                                SpecQuantity = (resource?.PackageSize > 0)
                                    ? $"{(int)(quantity / resource.PackageSize)} {package.SubCategoryName}" +
                                      $"{(quantity % resource.PackageSize > 0 ? $" (lẻ {quantity % resource.PackageSize} {unit.SubCategoryName})" : "")}"
                                    : $"{(int)quantity} {unit.SubCategoryName}",
                                UnitSpecification = $"{resource.PackageSize} {unit.SubCategoryName}/{package.SubCategoryName}",
                                CurrentSupplierId = rs.Supplier.SupplierId,
                                CurrentSupplierName = rs.Supplier.SupplierName,
                                CurrentSupplierCode = rs.Supplier.SupplierCode,
                                ResourceSupplierId = rs.ResourceSupplierId,
                                SupplierName = resourceSuppliers.Count > 0 ? $"Có {resourceSuppliers.Count} nhà cung cấp" : "Không rõ nguồn gốc",
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
                        }

                        if (typeName == "equipment" && resource.WareStocks.Any(x => x.SupplierId == rs.Supplier.SupplierId))
                        {
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
                                SpecQuantity = (resource?.PackageSize > 0)
                                    ? $"{(int)(quantity / resource.PackageSize)} {package.SubCategoryName}" +
                                      $"{(quantity % resource.PackageSize > 0 ? $" (lẻ {quantity % resource.PackageSize} {unit.SubCategoryName})" : "")}"
                                    : $"{(int)quantity} {unit.SubCategoryName}",
                                UnitSpecification = $"{resource.PackageSize} {unit.SubCategoryName}/{package.SubCategoryName}",
                                CurrentSupplierId = rs.Supplier.SupplierId,
                                CurrentSupplierName = rs.Supplier.SupplierName,
                                CurrentSupplierCode = rs.Supplier.SupplierCode,
                                ResourceSupplierId = rs.ResourceSupplierId,
                                SupplierName = resourceSuppliers.Count > 0 ? $"Có {resourceSuppliers.Count} nhà cung cấp" : "Không rõ nguồn gốc",
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
                        }

                        if (typeName == "medicine" && resource.WareStocks.Any(x => x.SupplierId == rs.Supplier.SupplierId))
                        {
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
                                SpecQuantity = (resource?.PackageSize > 0)
                                    ? $"{(int)(quantity / resource.PackageSize)} {package.SubCategoryName}" +
                                      $"{(quantity % resource.PackageSize > 0 ? $" (lẻ {quantity % resource.PackageSize} {unit.SubCategoryName})" : "")}"
                                    : $"{(int)quantity} {unit.SubCategoryName}",
                                UnitSpecification = $"{resource.PackageSize} {unit.SubCategoryName}/{package.SubCategoryName}",
                                CurrentSupplierId = rs.Supplier.SupplierId,
                                CurrentSupplierName = rs.Supplier.SupplierName,
                                CurrentSupplierCode = rs.Supplier.SupplierCode,
                                ResourceSupplierId = rs.ResourceSupplierId,
                                SupplierName = resourceSuppliers.Count > 0 ? $"Có {resourceSuppliers.Count} nhà cung cấp" : "Không rõ nguồn gốc",
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
                        }

                        if (typeName == "breeding" && resource.WareStocks.Any(x => x.SupplierId == rs.Supplier.SupplierId))
                        {
                            var existChickenTypeName = _unitOfWork.SubCategoryRepository.Get(filter: f => f.SubCategoryId.Equals(resource.Chicken.ChickenTypeId) && f.IsDeleted == false).FirstOrDefault();

                            return new WareStockChickenBreedingResponse
                            {
                                ResourceId = resource?.ResourceId ?? Guid.Empty,
                                ChickenId = resource?.ChickenId ?? Guid.Empty,
                                ChickenCode = resource?.Chicken?.ChickenCode,
                                ChickenName = resource?.Chicken?.ChickenName,
                                Description = resource?.Chicken?.Description,
                                ChickenTypeName = existChickenTypeName?.SubCategoryName,
                                SpecQuantity = (resource?.PackageSize > 0)
                                    ? $"{(int)(quantity / resource.PackageSize)} {package.SubCategoryName}" +
                                      $"{(quantity % resource.PackageSize > 0 ? $" (lẻ {quantity % resource.PackageSize} {unit.SubCategoryName})" : "")}"
                                    : $"{(int)quantity} {unit.SubCategoryName}",
                                UnitSpecification = $"{resource.PackageSize} {unit.SubCategoryName}/{package.SubCategoryName}",
                                CurrentSupplierId = rs.Supplier.SupplierId,
                                CurrentSupplierName = rs.Supplier.SupplierName,
                                CurrentSupplierCode = rs.Supplier.SupplierCode,
                                ResourceSupplierId = rs.ResourceSupplierId,
                                SupplierName = resourceSuppliers.Count > 0 ? $"Có {resourceSuppliers.Count} nhà cung cấp" : "Không rõ nguồn gốc",
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
                        }

                        if (typeName == "harvest_product" && resource.WareStocks.Any(x => x.SupplierId == rs.Supplier.SupplierId))
                        {
                            var existHarvestProductType = _unitOfWork.SubCategoryRepository.Get(filter: f => f.SubCategoryId.Equals(resource.HarvestProduct.HarvestProductTypeId) && f.IsDeleted == false).FirstOrDefault();

                            return new WareStockHavestProductResponse
                            {
                                ResourceId = resource?.ResourceId ?? Guid.Empty,
                                HarvestProductId = resource?.HarvestProductId ?? Guid.Empty,
                                HarvestProductCode = resource?.HarvestProduct?.HarvestProductCode,
                                HarvestProductName = resource?.HarvestProduct?.HarvestProductName,
                                HarvestProductTypeId = existHarvestProductType?.SubCategoryId,
                                HarvestProductTypeName = existHarvestProductType?.SubCategoryName,
                                SpecQuantity = (resource?.PackageSize > 0)
                                    ? $"{(int)(quantity / resource.PackageSize)} {package.SubCategoryName}" +
                                      $"{(quantity % resource.PackageSize > 0 ? $" (lẻ {quantity % resource.PackageSize} {unit.SubCategoryName})" : "")}"
                                    : $"{(int)quantity} {unit.SubCategoryName}",
                                UnitSpecification = $"{resource.PackageSize} {unit.SubCategoryName}/{package.SubCategoryName}",
                                CurrentSupplierId = rs.Supplier.SupplierId,
                                CurrentSupplierName = rs.Supplier.SupplierName,
                                CurrentSupplierCode = rs.Supplier.SupplierCode,
                                ResourceSupplierId = rs.ResourceSupplierId,
                                SupplierName = resourceSuppliers.Count > 0 ? $"Có {resourceSuppliers.Count} nhà cung cấp" : "Không rõ nguồn gốc",
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
                        }

                        return null;
                    });

                return result;
            })
            .Where(x => x != null)
            .ToList();
            return BaseResponse<IEnumerable<object>>.SuccessResponse(responses);
        }
    }
}
