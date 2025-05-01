using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.DTOs.Supplier;
using CFMS.Application.DTOs.WareStock;
using CFMS.Application.Features.WarehouseFeat.GetWareStock;
using CFMS.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.WarehouseFeat.GetWareStockByWareId
{
    public class GetWareStockByWareIdQueryHandler : IRequestHandler<GetWareStockByWareIdQuery, BaseResponse<object>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetWareStockByWareIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<object>> Handle(GetWareStockByWareIdQuery request, CancellationToken cancellationToken)
        {
            var resources = _unitOfWork.ResourceRepository.Get(
                filter: f => f.ResourceId.Equals(request.ResourceId) && f.IsDeleted == false,
                includeProperties: [
                    r => r.ResourceSuppliers,
                    r => r.Food,
                    r => r.Equipment,
                    r => r.Medicine,
                    r => r.HarvestProduct,
                    r => r.Chicken,
                    r => r.WareStocks
                    ]
            ).ToList();

            if (resources.Count == 0)
            {
                return BaseResponse<object>.FailureResponse("Hàng hoá không tồn tại");
            }

            var existResourceType = _unitOfWork.SubCategoryRepository.Get(filter: f => f.SubCategoryId.Equals(resources.FirstOrDefault().ResourceTypeId) && f.IsDeleted == false).FirstOrDefault();
            if (existResourceType == null)
            {
                return BaseResponse<object>.FailureResponse("Loại hàng hoá không tồn tại");
            }

            var wareStockFoodResponses = resources
                    .Select(resource =>
                    {
                        var unit = _unitOfWork.SubCategoryRepository.Get(filter: f => f.SubCategoryId.Equals(resource.UnitId) && f.IsDeleted == false).FirstOrDefault();

                        var package = _unitOfWork.SubCategoryRepository.Get(filter: f => f.SubCategoryId.Equals(resource.PackageId) && f.IsDeleted == false).FirstOrDefault();

                        if (resource == null || unit == null || package == null)
                        {
                            return null;
                        }

                        var quantity = resource?.WareStocks.FirstOrDefault(x => x.ResourceId.Equals(resource.ResourceId) && x.WareId.Equals(request.WareId))?.Quantity ?? 0;

                        var resourceSuppliers = _unitOfWork.ResourceSupplierRepository
                            .GetIncludeMultiLayer(
                                filter: f => f.ResourceId.Equals(resource.ResourceId)
                                          && f.IsDeleted == false,
                                include: x => x.Include(t => t.Supplier)
                            ).ToList();

                        switch (existResourceType.SubCategoryName)
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
                                    UnitSpecification = $"{resource.PackageSize} {unit.SubCategoryName}/{package.SubCategoryName}",
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
                                    HarvestProductCode = resource?.HarvestProduct?.HarvestProductCode,
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
                .ToList();

            return BaseResponse<object>.SuccessResponse(wareStockFoodResponses);
        }
    }
}
