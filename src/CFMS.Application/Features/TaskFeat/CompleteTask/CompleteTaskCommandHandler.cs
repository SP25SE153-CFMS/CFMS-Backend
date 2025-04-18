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
                ).FirstOrDefault();

            if (existTask == null)
            {
                return BaseResponse<bool>.FailureResponse(message: "Công việc không tồn tại");
            }

            var taskType = _unitOfWork.SubCategoryRepository.Get(filter: x => x.SubCategoryId.Equals(existTask.TaskTypeId) && x.IsDeleted == false).FirstOrDefault()?.SubCategoryName;

            try
            {
                string[] keywords = { "thực phẩm", "dược phẩm", "thiết bị", "thu hoạch", "con giống" };

                existTask.Status = 1;
                //var leader = existTask.Assignments.Where(x => x.Status.Equals(1)).FirstOrDefault();
                //leader.Note = request.Note;

                var requestType = _unitOfWork.SubCategoryRepository.Get(x => x.SubCategoryName.Equals("IMPORT") && x.IsDeleted == false).FirstOrDefault();

                //var lastRequest = _unitOfWork.RequestRepository.Get(filter: r => r.CreatedByUser.UserId.ToString().Equals(leader.AssignedToId)).FirstOrDefault();
                var newRequest = new Request()
                {
                    RequestTypeId = requestType?.SubCategoryId,
                    Status = 0
                };

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
                        Note = request.Note,
                    };

                    _unitOfWork.TaskLogRepository.Insert(taskLog);
                }

                if (taskType.Equals("feed"))
                {

                }
            }
        }
    }
}
