﻿using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CFMS.Application.Events.Handlers
{
    public class StockUpdatedEventHandler : INotificationHandler<StockUpdatedEvent>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EventQueue _eventQueue;

        public StockUpdatedEventHandler(IUnitOfWork unitOfWork, EventQueue eventQueue)
        {
            _unitOfWork = unitOfWork;
            _eventQueue = eventQueue;
        }

        public async System.Threading.Tasks.Task Handle(StockUpdatedEvent notification, CancellationToken cancellationToken)
        {
            var resource = _unitOfWork.ResourceRepository.Get
                (filter: x => (x.FoodId.Equals(notification.ResourceId) 
                || x.EquipmentId.Equals(notification.ResourceId) 
                || x.MedicineId.Equals(notification.ResourceId)
                || x.ChickenId.Equals(notification.ResourceId)
                || x.HarvestProductId.Equals(notification.ResourceId))
                    && x.UnitId.Equals(notification.UnitId)
                    && x.PackageId.Equals(notification.PackageId)
                    && x.PackageSize.Equals(notification.PackageSize)
                    && x.IsDeleted == false).FirstOrDefault();

            var resourceType = _unitOfWork.SubCategoryRepository.Get(filter: x => x.SubCategoryName.Equals(notification.ResourceType) && x.IsDeleted == false).FirstOrDefault();
            if (resourceType == null)
            {
                throw new Exception("Không tìm thấy loại hàng hoá");
            }

            if (notification.IsCreatedCall && resource == null)
            {
                resource = new Resource
                {
                    ResourceTypeId = resourceType.SubCategoryId,
                    UnitId = notification.UnitId,
                    PackageId = notification.PackageId,
                    PackageSize = notification.PackageSize
                };

                if (notification.ResourceType.Equals("food"))
                    resource.FoodId = notification.ResourceId;

                if (notification.ResourceType.Equals("medicine"))
                    resource.MedicineId = notification.ResourceId;

                if (notification.ResourceType.Equals("equipment"))
                    resource.EquipmentId = notification.ResourceId;

                if (notification.ResourceType.Equals("harvest_product"))
                    resource.HarvestProductId = notification.ResourceId;

                if (notification.ResourceType.Equals("breeding"))
                    resource.ChickenId = notification.ResourceId;

                await _unitOfWork.ResourceRepository.UpdateOrInsertAsync(resource);
                await _unitOfWork.SaveChangesAsync();
            }

            var ware = await _unitOfWork.WarehouseRepository
                  .FirstOrDefaultAsync(x => x.WareId.Equals(notification.WareId));

            if (ware == null)
            {
                throw new Exception("Không tìm thấy kho");
            }

            var wareStock = await _unitOfWork.WareStockRepository
                .FirstOrDefaultAsync(x => x.WareId.Equals(notification.WareId) && x.ResourceId.Equals(resource.ResourceId) && x.SupplierId == notification.SupplierId);


            if (notification.IsCreatedCall && resource != null && wareStock != null)
            {
                throw new Exception("Hàng hoá có quy cách tính này đã tồn tại");
            }


            //var tempStock = await _unitOfWork.WareStockRepository
            //      .FirstOrDefaultAsync(x => x.WareId.Equals(notification.WareId) && x.ResourceId.Equals(resource.ResourceId) && x.SupplierId == null);

            //if (tempStock != null)
            //{
            //    _unitOfWork.WareStockRepository.Delete(tempStock);
            //}

            wareStock ??= new WareStock
            {
                WareId = notification.WareId,
                ResourceId = resource?.ResourceId,
                Quantity = 0,
                SupplierId = notification.SupplierId
            };

            ware.CurrentQuantity += notification.Quantity;
            wareStock.Quantity += notification.Quantity;
            await _unitOfWork.WarehouseRepository.UpdateOrInsertAsync(ware);
            await _unitOfWork.WareStockRepository.UpdateOrInsertAsync(wareStock);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
