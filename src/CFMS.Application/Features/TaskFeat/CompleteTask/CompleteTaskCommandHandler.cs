using AutoMapper;
using CFMS.Application.Common;
using CFMS.Application.DTOs.TaskResource;
using CFMS.Application.Events;
using CFMS.Domain.Entities;
using CFMS.Domain.Enums.Types;
using CFMS.Domain.Interfaces;
using Google.Apis.Drive.v3.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Org.BouncyCastle.Asn1.Cmp;
using Org.BouncyCastle.Utilities;
using Twilio.Base;
using Resource = CFMS.Domain.Entities.Resource;

namespace CFMS.Application.Features.TaskFeat.CompleteTask
{
    public class CompleteTaskCommandHandler : IRequestHandler<CompleteTaskCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CompleteTaskCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<bool>> Handle(CompleteTaskCommand request, CancellationToken cancellationToken)
        {
            var existTask = _unitOfWork.TaskRepository.GetIncludeMultiLayer(filter: t => t.TaskId.Equals(request.TaskId) && t.IsDeleted == false,
                include: q => q
                    .Include(t => t.Assignments)
                    .Include(t => t.TaskType)
                    .Include(t => t.TaskHarvests)
                    .Include(t => t.TaskResources)
                        .ThenInclude(s => s.Resource)
                            .ThenInclude(r => r.Food)
                    .Include(t => t.TaskResources)
                        .ThenInclude(s => s.Resource)
                            .ThenInclude(r => r.Medicine)
                    .Include(t => t.TaskResources)
                        .ThenInclude(s => s.Resource)
                            .ThenInclude(r => r.Equipment)
                    .Include(t => t.TaskResources)
                        .ThenInclude(s => s.Resource)
                            .ThenInclude(r => r.HarvestProduct)
                    .Include(t => t.TaskResources)
                        .ThenInclude(s => s.Resource)
                            .ThenInclude(r => r.Chicken)
                    .Include(t => t.TaskResources)
                        .ThenInclude(s => s.ResourceType)
                    .Include(t => t.TaskResources)
                        .ThenInclude(s => s.Resource)
                            .ThenInclude(s => s.Unit)
                    .Include(t => t.TaskResources)
                        .ThenInclude(s => s.Resource)
                            .ThenInclude(s => s.Package)
                    .Include(t => t.TaskResources)
                        .ThenInclude(s => s.Unit)
                    .Include(t => t.TaskLocations)
                        .ThenInclude(s => s.Location)
                    .Include(t => t.TaskLocations)
                        .ThenInclude(s => s.LocationNavigation)
                    .Include(t => t.FeedLogs)
                    .Include(t => t.VaccineLogs)
                    .Include(t => t.TaskLogs)
                    .Include(t => t.HealthLogs)
                ).FirstOrDefault();

            if (existTask == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Công việc không tồn tại");
            }

            if (existTask.Status.Equals(1))
            {
                return BaseResponse<bool>.FailureResponse(message: "Công việc này đã được báo cáo rồi");
            }

            var taskType = _unitOfWork.SubCategoryRepository.Get(filter: x => x.SubCategoryId.Equals(existTask.TaskTypeId) && x.IsDeleted == false).FirstOrDefault()?.SubCategoryName;

            var totalConsumed = request?.TaskResources?.Sum(x => x.ConsumedQuantity) ?? 0;
            var totalSupplied = request?.TaskResources?.Sum(x => x.SuppliedQuantity) ?? 0;
            var isRedundant = totalSupplied - totalConsumed > 0 ? true : false;

            try
            {
                //string[] keywords = { "thực phẩm", "dược phẩm", "thiết bị", "thu hoạch", "con giống" };

                existTask.Status = 1;
                //var leader = existTask.Assignments.Where(x => x.Status.Equals(1)).FirstOrDefault();
                //leader.Note = request.Note;

                var requestType = _unitOfWork.SubCategoryRepository.Get(x => x.SubCategoryName.Equals("IMPORT") && x.IsDeleted == false).FirstOrDefault();

                //var lastRequest = _unitOfWork.RequestRepository.Get(filter: r => r.CreatedByUser.UserId.ToString().Equals(leader.AssignedToId)).FirstOrDefault();

                var newRequest = new Request()
                {
                    RequestTypeId = requestType?.SubCategoryId,
                    FarmId = existTask.FarmId,
                    Status = 0
                };

                if (isRedundant || (request?.HarvestProducts?.ToList().Count > 0))
                {
                    _unitOfWork.RequestRepository.Insert(newRequest);
                    await _unitOfWork.SaveChangesAsync();

                    if (request?.TaskResources != null)
                    {
                        Resource resource;

                        var groupedResources = request?.TaskResources?
                        .Select(detail => new
                        {

                            Resource = _unitOfWork.ResourceRepository.GetIncludeMultiLayer(
                                filter: r => r.ResourceId == detail.ResourceId,
                                include: x => x
                                    .Include(t => t.Food)
                                    .Include(t => t.Equipment)
                                    .Include(t => t.Medicine)
                                    .Include(t => t.Chicken)
                                    .Include(t => t.HarvestProduct)
                                    .Include(t => t.ResourceType)
                                    .Include(t => t.ResourceSuppliers))
                                    .FirstOrDefault(),
                            SuppliedQuantity = detail.SuppliedQuantity,
                            ConsumedQuantity = detail.ConsumedQuantity,
                        })
                        .GroupBy(x =>
                        {
                            var resource = x.Resource;
                            if (resource?.Food != null) return "Kho thực phẩm";
                            if (resource?.Medicine != null) return "Kho dược phẩm";
                            if (resource?.Equipment != null) return "Kho thiết bị";
                            if (resource?.Chicken != null) return "Kho con giống";
                            if (resource?.HarvestProduct != null) return "Kho thu hoạch";
                            return "Kho khác";
                        })
                        .ToList();

                        foreach (var group in groupedResources)
                        {
                            var ware = _unitOfWork.WarehouseRepository
                                .Get(filter: w => w.WarehouseName.Equals(group.Key) && w.IsDeleted == false)
                                .FirstOrDefault();

                            var inventoryRequest = new InventoryRequest
                            {
                                RequestId = newRequest.RequestId,
                                InventoryRequestTypeId = requestType?.SubCategoryId,
                                WareToId = ware?.WareId,
                            };

                            _unitOfWork.InventoryRequestRepository.Insert(inventoryRequest);
                            await _unitOfWork.SaveChangesAsync();

                            foreach (var detail in group)
                            {
                                var inventoryRequestDetail = new InventoryRequestDetail
                                {
                                    InventoryRequestId = inventoryRequest.InventoryRequestId,
                                    ResourceId = detail?.Resource?.ResourceId,
                                    ResourceSupplierId = detail?.Resource?.ResourceSuppliers?.Where(t => t.ResourceId.Equals(detail?.Resource?.ResourceId)).FirstOrDefault()?.ResourceSupplierId,
                                    ExpectedQuantity = detail?.SuppliedQuantity - detail?.ConsumedQuantity,
                                    UnitId = detail?.Resource?.UnitId,
                                    Reason = request?.Reason,
                                    ExpectedDate = DateTime.Now.ToLocalTime(),
                                    Note = request?.Note
                                };

                                _unitOfWork.InventoryRequestDetailRepository.Insert(inventoryRequestDetail);
                            }

                            await _unitOfWork.SaveChangesAsync();
                        }
                    }
                }

                if (request?.HarvestProducts != null)
                {
                    Resource resource;

                    var groupedResources = request?.HarvestProducts?
                    .Select(detail => new
                    {

                        Resource = _unitOfWork.ResourceRepository.GetIncludeMultiLayer(
                            filter: r => r.ResourceId == detail.ResourceId,
                            include: x => x
                                .Include(t => t.HarvestProduct))
                                .FirstOrDefault(),
                        HarvestQuantity = detail.Quantity
                    })
                    .GroupBy(x => new
                    {
                        x?.Resource?.PackageSize,
                        x?.Resource?.UnitId,
                        x?.Resource?.PackageId
                    })
                    .ToList();


                    foreach (var group in groupedResources)
                    {
                        var resourceType = _unitOfWork.SubCategoryRepository
                            .Get(filter: w => w.SubCategoryName.Equals("harvest_product") && w.IsDeleted == false)
                            .FirstOrDefault();

                        var ware = _unitOfWork.WarehouseRepository
                            .Get(filter: w => w.ResourceTypeId.Equals(resourceType.SubCategoryId) && w.IsDeleted == false)
                            .FirstOrDefault();

                        var inventoryRequest = new InventoryRequest
                        {
                            RequestId = newRequest.RequestId,
                            InventoryRequestTypeId = requestType?.SubCategoryId,
                            WareToId = ware?.WareId,
                        };

                        _unitOfWork.InventoryRequestRepository.Insert(inventoryRequest);
                        await _unitOfWork.SaveChangesAsync();

                        foreach (var detail in group)
                        {
                            var inventoryRequestDetail = new InventoryRequestDetail
                            {
                                InventoryRequestId = inventoryRequest.InventoryRequestId,
                                ResourceId = detail?.Resource?.ResourceId,
                                ResourceSupplierId = detail?.Resource?.ResourceSuppliers?.Where(t => t.ResourceId.Equals(detail?.Resource?.ResourceId)).FirstOrDefault()?.ResourceSupplierId,
                                ExpectedQuantity = detail?.HarvestQuantity,
                                UnitId = detail?.Resource?.UnitId,
                                Reason = request?.Reason,
                                ExpectedDate = DateTime.Now.ToLocalTime(),
                                Note = request?.Note
                            };

                            _unitOfWork.InventoryRequestDetailRepository.Insert(inventoryRequestDetail);
                        }

                        await _unitOfWork.SaveChangesAsync();
                    }
                }

                _unitOfWork.TaskRepository.Update(existTask);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return BaseResponse<bool>.SuccessResponse(message: "Cập nhật công việc thành công");
                }
                return BaseResponse<bool>.FailureResponse(message: "Cập nhật không thành công");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(message: ex.Message);
            }
            finally
            {
                var location = existTask.TaskLocations.FirstOrDefault();
                if (location?.LocationType?.Equals("COOP") == true)
                {
                    var taskLog = new TaskLog
                    {
                        ChickenCoopId = location.CoopId,
                        CompletedAt = DateTime.Now.ToLocalTime(),
                        TaskId = existTask.TaskId,
                        Note = request?.Note,
                    };

                    _unitOfWork.TaskLogRepository.Insert(taskLog);
                }

                if (taskType.Equals("feed"))
                {
                    var coop = _unitOfWork.ChickenCoopRepository.Get(filter: x => x.ChickenCoopId.Equals(location.CoopId) && x.IsDeleted == false, includeProperties: "ChickenBatches").FirstOrDefault();

                    var unit = _ = _unitOfWork.SubCategoryRepository.Get(filter: x => x.SubCategoryName.Contains("kg") && x.IsDeleted == false).FirstOrDefault();

                    var groupResources = request?.TaskResources
                        .Select(detail => new
                        {
                            Resource = _unitOfWork.ResourceRepository.GetIncludeMultiLayer(
                            filter: r => r.ResourceId == detail.ResourceId,
                            include: x => x
                                .Include(t => t.HarvestProduct)
                                .Include(t => t.Food)
                                .Include(t => t.Medicine)
                                .Include(t => t.Chicken)
                                .Include(t => t.Equipment))
                                .FirstOrDefault(),
                            ConsumedQuantity = detail.ConsumedQuantity,
                        })
                        .GroupBy(x => new
                        {
                            x?.Resource?.ResourceId
                        })
                        .ToList();

                    foreach (var group in groupResources)
                    {
                        foreach (var detail in group)
                        {
                            var feedLog = new FeedLog
                            {
                                ChickenBatchId = coop?.ChickenBatches
                                                    .Where(x => x.EndDate == null)
                                                    .OrderByDescending(x => x.StartDate)
                                                    .Select(x => x.ChickenBatchId)
                                                    .FirstOrDefault(),
                                FeedingDate = DateTime.Now.ToLocalTime().AddHours(7),
                                ActualFeedAmount = detail.ConsumedQuantity,
                                UnitId = unit?.SubCategoryId,
                                TaskId = request?.TaskId,
                                Note = request?.Note,
                                ResourceId = detail.Resource?.ResourceId
                            };

                            _unitOfWork.FeedLogRepository.Insert(feedLog);
                        }
                    }
                }

                if (taskType.Equals("inject"))
                {
                    var coop = _unitOfWork.ChickenCoopRepository.Get(filter: x => x.ChickenCoopId.Equals(location.CoopId) && x.IsDeleted == false, includeProperties: "ChickenBatches").FirstOrDefault();

                    var vaccineLog = new VaccineLog
                    {
                        Notes = request.Note,
                        Status = 1,
                        Reaction = request.Reaction,
                        ChickenBatchId = coop?.ChickenBatches
                                            .Where(x => x.EndDate == null)
                                            .OrderByDescending(x => x.StartDate)
                                            .Select(x => x.ChickenBatchId)
                                            .FirstOrDefault(),
                        TaskId = request.TaskId
                    };

                    _unitOfWork.VaccineLogRepository.Insert(vaccineLog);
                }

                if (taskType.Equals("harvest"))
                {
                    foreach (var item in request.HarvestProducts)
                    {
                        var resource = _unitOfWork.ResourceRepository.GetIncludeMultiLayer(filter: x => x.ResourceId.Equals(item.ResourceId) && x.IsDeleted == false,
                            include: x => x
                                .Include(t => t.HarvestProduct)
                                .Include(t => t.Food)
                                .Include(t => t.Medicine)
                                .Include(t => t.Chicken)
                                .Include(t => t.Equipment)
                            ).FirstOrDefault();

                        var harvestProduct = new TaskHarvest
                        {
                            HarvestProductId = item.ResourceId ?? Guid.Empty,
                            HarvestTypeId = resource?.HarvestProduct?.HarvestProductTypeId,
                            TaskId = request.TaskId,
                            Quantity = item.Quantity
                        };

                        _unitOfWork.TaskHarvestRepository.Insert(harvestProduct);
                    }
                }

                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
