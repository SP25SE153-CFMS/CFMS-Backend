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
            var resource = await _unitOfWork.ResourceRepository
                .FirstOrDefaultAsync(x => x.FoodId == notification.ResourceId
                    && x.UnitId == notification.UnitId
                    && x.PackageId == notification.PackageId
                    && x.PackageSize == notification.PackageSize
                    && !x.IsDeleted);

            if (notification.IsCreatedCall)
            {
                if (resource != null)
                    throw new Exception("Sản phẩm có quy cách tính như này đã tồn tại");

                resource = new Resource
                {
                    ResourceTypeId = notification.ResourceTypeId,
                    UnitId = notification.UnitId,
                    PackageId = notification.PackageId,
                    PackageSize = notification.PackageSize,
                    FoodId = notification.ResourceId
                };

                await _unitOfWork.ResourceRepository.InsertAsync(resource);
                await _unitOfWork.SaveChangesAsync();
            }
            else if (resource == null)
            {
                throw new Exception("Sản phẩm có quy cách tính như này không tồn tại");
            }

            var wareStock = await _unitOfWork.WareStockRepository
                .FirstOrDefaultAsync(x => x.WareId == notification.WareId && x.ResourceId == resource.ResourceId);

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
