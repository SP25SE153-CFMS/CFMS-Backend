using CFMS.Application.Common;
using CFMS.Application.DTOs.Supplier;
using CFMS.Application.DTOs.WareStock;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.SupplierFeat.GetResourceSuppliers
{
    public class GetResourceSuppliersQueryHandler : IRequestHandler<GetResourceSuppliersQuery, BaseResponse<IEnumerable<object>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetResourceSuppliersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<IEnumerable<object>>> Handle(GetResourceSuppliersQuery request, CancellationToken cancellationToken)
        {
            var suppliers = _unitOfWork.SupplierRepository.Get(
                    filter: f => f.SupplierId.Equals(request.SupplierId) && f.IsDeleted == false,
                    includeProperties: [
                        r => r.ResourceSuppliers
                        ]
                ).ToList();

            if (suppliers.Count == 0)
            {
                return BaseResponse<IEnumerable<object>>.FailureResponse("Nhà cung cấp không tồn tại");
            }

            var resourceSupplierResponses = suppliers
                .SelectMany(r => r.ResourceSuppliers, (supplier, resource) =>
                {
                    var existResource = _unitOfWork.ResourceRepository.Get(filter: f => f.ResourceId.Equals(resource.ResourceId) && f.IsDeleted == false,
                        includeProperties: [
                            r => r.Food,
                            r => r.Equipment,
                            r => r.Medicine
                            ]).FirstOrDefault();

                    var existResourceType = _unitOfWork.SubCategoryRepository.Get(filter: f => f.SubCategoryId.Equals(existResource.ResourceTypeId) && f.IsDeleted == false).FirstOrDefault();
                    if (existResourceType == null)
                    {
                        return null;
                    }

                    var unit = _unitOfWork.SubCategoryRepository.Get(filter: f => f.SubCategoryId.Equals(existResource.UnitId) && f.IsDeleted == false).FirstOrDefault();

                    var package = _unitOfWork.SubCategoryRepository.Get(filter: f => f.SubCategoryId.Equals(existResource.PackageId) && f.IsDeleted == false).FirstOrDefault();

                    if (resource == null || unit == null || package == null || supplier == null)
                    {
                        return null;
                    }

                    var resourceSupplier = _unitOfWork.ResourceSupplierRepository.Get(filter: f => f.ResourceId.Equals(resource.ResourceId) && f.Supplier.SupplierId.Equals(request.SupplierId) && f.IsDeleted == false).FirstOrDefault();

                    switch (existResourceType.SubCategoryName)
                    {
                        case "food":
                            return new ResourceSupplierFoodResponse
                            {
                                FoodCode = existResource?.Food.FoodCode ?? "Không xác định",
                                FoodName = existResource?.Food.FoodName ?? "Không xác định",
                                ResourceType = "Thực phẩm",
                                Note = existResource?.Food.Note,
                                ProductionDate = existResource?.Food.ProductionDate,
                                ExpiryDate = existResource?.Food.ExpiryDate,
                                UnitSpecification = $"{existResource.PackageSize} {unit.SubCategoryName}/{package.SubCategoryName}",
                                Description = resourceSupplier?.Description,
                                Price = resourceSupplier?.Price
                            } as ResourceSupplierResponseBase;

                        case "equipment":
                            var existMaterial = _unitOfWork.SubCategoryRepository.Get(filter: f => f.SubCategoryId.Equals(existResource.Equipment.MaterialId) && f.IsDeleted == false).FirstOrDefault();

                            return new ResourceSupplierEquipmentResponse
                            {
                                EquipmentCode = existResource?.Equipment.EquipmentCode ?? "Không xác định",
                                EquipmentName = existResource?.Equipment.EquipmentName ?? "Không xác định",
                                ResourceType = "Thiết bị",
                                Material = existMaterial.SubCategoryName,
                                Usage = existResource?.Equipment.Usage,
                                Warranty = existResource?.Equipment.Warranty,
                                Size = 0,
                                Weight = 0,
                                PurchaseDate = existResource?.Equipment.PurchaseDate,
                                UnitSpecification = $"{existResource.PackageSize} {unit.SubCategoryName}/{package.SubCategoryName}",
                                Description = resourceSupplier?.Description,
                                Price = resourceSupplier?.Price
                            };

                        case "medicine":
                            var existDisease = _unitOfWork.SubCategoryRepository.Get(filter: f => f.SubCategoryId.Equals(existResource.Medicine.DiseaseId) && f.IsDeleted == false).FirstOrDefault();

                            return new ResourceSupplierMedicineResponse
                            {
                                MedicineCode = existResource?.Medicine.MedicineCode ?? "Không xác định",
                                MedicineName = existResource?.Medicine.MedicineName ?? "Không xác định",
                                ResourceType = "Dược phẩm",
                                Usage = existResource?.Medicine.Usage,
                                DosageForm = existResource?.Medicine.DosageForm,
                                StorageCondition = existResource?.Medicine.StorageCondition,
                                Disease = existDisease.SubCategoryName,
                                ProductionDate = existResource?.Medicine.ProductionDate,
                                ExpiryDate = existResource?.Medicine.ExpiryDate,
                                UnitSpecification = $"{existResource.PackageSize} {unit.SubCategoryName}/{package.SubCategoryName}",
                                Description = resourceSupplier?.Description,
                                Price = resourceSupplier?.Price
                            };

                        default:
                            return null;
                    }
                })
                .Where(x => x != null)
                .ToList();

            return BaseResponse<IEnumerable<object>>.SuccessResponse(resourceSupplierResponses);
        }
    }
}
