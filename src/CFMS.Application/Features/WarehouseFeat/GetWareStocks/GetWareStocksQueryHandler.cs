using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.DTOs.WareStock;
using CFMS.Domain.Interfaces;
using MediatR;

namespace CFMS.Application.Features.WarehouseFeat.GetWareStocks
{
    public class GetWareStocksQueryHandler : IRequestHandler<GetWareStocksQuery, BaseResponse<IEnumerable<object>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetWareStocksQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<IEnumerable<object>>> Handle(GetWareStocksQuery request, CancellationToken cancellationToken)
        {
            var existResourceType = _unitOfWork.SubCategoryRepository.Get(filter: f => f.SubCategoryId.Equals(request.ResourceTypeId) && f.IsDeleted == false).FirstOrDefault();
            if (existResourceType == null)
            {
                return BaseResponse<IEnumerable<object>>.FailureResponse("Loại hàng hoá không tồn tại");
            }

            var resources = _unitOfWork.ResourceRepository.Get(
                filter: f => f.ResourceTypeId.Equals(request.ResourceTypeId) && f.IsDeleted == false,
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
                return BaseResponse<IEnumerable<object>>.FailureResponse("Không có hàng hoá nào");
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

                    var quantity = resource?.WareStocks.FirstOrDefault(x => x.ResourceId.Equals(resource.ResourceId) && x.WareId.Equals(request.WareId))?.Quantity ?? 0;

                    var ware = _unitOfWork.WarehouseRepository.Get(filter: f => f.WareId.Equals(request.WareId) && f.IsDeleted == false).FirstOrDefault();

                    var resourceSupplier = _unitOfWork.ResourceSupplierRepository.Get(filter: f => f.ResourceId.Equals(resource.ResourceId) && f.Supplier.FarmId.Equals(ware.FarmId) && f.IsDeleted == false).FirstOrDefault();

                    switch (existResourceType.SubCategoryName)
                    {
                        case "food":
                            return (WareStockResponseBase) new WareStockFoodResponse
                            {
                                ResourceId = resource?.ResourceId ?? Guid.Empty,
                                FoodId = resource?.FoodId ?? Guid.Empty,
                                FoodCode = resource?.Food?.FoodCode,
                                FoodName = resource?.Food?.FoodName,
                                Note = resource?.Food?.Note,
                                ProductionDate = resource?.Food?.ProductionDate,
                                ExpiryDate = resource?.Food?.ExpiryDate,
                                SpecQuantity = $"{quantity} {package.SubCategoryName} ({resource?.PackageSize * quantity} {unit.SubCategoryName})",
                                UnitSpecification = $"{resource?.PackageSize} {unit.SubCategoryName}/{package.SubCategoryName}",
                                SupplierName = resourceSupplier?.Supplier?.SupplierName ?? "Chưa có nhà cung cấp"
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
                                SpecQuantity = $"{quantity} {package.SubCategoryName} ({resource?.PackageSize * quantity} {unit.SubCategoryName})",
                                UnitSpecification = $"{resource?.PackageSize} {unit.SubCategoryName}/{package.SubCategoryName}",
                                SupplierName = resourceSupplier?.Supplier?.SupplierName ?? "Chưa có nhà cung cấp"
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
                                SpecQuantity = $"{quantity} {package.SubCategoryName} ({resource?.PackageSize * quantity} {unit.SubCategoryName})",
                                UnitSpecification = $"{resource.PackageSize} {unit.SubCategoryName}/{package.SubCategoryName}",
                                SupplierName = resourceSupplier?.Supplier?.SupplierName ?? "Chưa có nhà cung cấp"
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
                                SpecQuantity = $"{quantity} {package.SubCategoryName} ({resource?.PackageSize * quantity} {unit.SubCategoryName})",
                                UnitSpecification = $"{resource?.PackageSize} {unit.SubCategoryName}/{package.SubCategoryName}",
                                SupplierName = resourceSupplier?.Supplier?.SupplierName ?? "Chưa có nhà cung cấp"
                            };

                        case "harvest_product":
                            var existHarvestProductType = _unitOfWork.SubCategoryRepository.Get(filter: f => f.SubCategoryId.Equals(resource.HarvestProduct.HarvestProductTypeId) && f.IsDeleted == false).FirstOrDefault();

                            return new WareStockHavestProductResponse
                            {
                                ResourceId = resource?.ResourceId ?? Guid.Empty,
                                HarvestProductId = resource?.HarvestProductId ?? Guid.Empty,
                                HarvestProductName = resource?.HarvestProduct?.HarvestProductName,
                                HarvestProductTypeId = existHarvestProductType.SubCategoryId,
                                HarvestProductTypeName = existHarvestProductType?.SubCategoryName,
                                SpecQuantity = $"{quantity} {package.SubCategoryName} ({resource?.PackageSize * quantity} {unit.SubCategoryName})",
                                UnitSpecification = $"{resource?.PackageSize} {unit.SubCategoryName}/{package.SubCategoryName}",
                                SupplierName = resourceSupplier?.Supplier?.SupplierName ?? "Chưa có nhà cung cấp"
                            };

                        default:
                            return null;
                    }
                })
                .Where(x => x != null)
                .ToList();

            return BaseResponse<IEnumerable<object>>.SuccessResponse(wareStockFoodResponses);
        }
    }
}
