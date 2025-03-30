using CFMS.Application.Common;
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
                (filter: x => (x.FoodId.Equals(notification.ResourceId) || x.EquipmentId.Equals(notification.ResourceId) || x.MedicineId.Equals(notification.ResourceId))
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

                await _unitOfWork.ResourceRepository.UpdateOrInsertAsync(resource);
                await _unitOfWork.SaveChangesAsync();
            }

            var wareStock = await _unitOfWork.WareStockRepository
                  .FirstOrDefaultAsync(x => x.WareId == notification.WareId && x.ResourceId == resource.ResourceId);

            if (notification.IsCreatedCall && resource != null && wareStock != null)
            {
                throw new Exception("Hàng hoá có quy cách tính này đã tồn tại");
            }

            wareStock ??= new WareStock
            {
                WareId = notification.WareId,
                ResourceId = resource.ResourceId,
                Quantity = 0
            };

            wareStock.Quantity += notification.Quantity;
            await _unitOfWork.WareStockRepository.UpdateOrInsertAsync(wareStock);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
