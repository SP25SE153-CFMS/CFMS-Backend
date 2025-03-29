using CFMS.Application.Common;
using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

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

        public async Task Handle(StockUpdatedEvent notification, CancellationToken cancellationToken)
        {
            if (notification.IsCreatedCall)
            {
                var existResource = _unitOfWork.ResourceRepository.Get(filter: x => x.ResourceId.Equals(notification.ResourceId) && x.IsDeleted == false).FirstOrDefault();
                if (existResource != null
                    && existResource.UnitId.Equals(notification.UnitId)
                    && existResource.PackageId.Equals(notification.PackageId)
                    && existResource.PackageSize.Equals(notification.PackageSize))
                {
                    throw new Exception("Sản phẩm có quy cách tính như này đã tồn tại");
                }

                var newResource = new Resource
                {
                    ResourceTypeId = notification.ResourceTypeId,
                    UnitId = notification.UnitId,
                    PackageId = notification.PackageId,
                    PackageSize = notification.PackageSize
                };

                _unitOfWork.ResourceRepository.Insert(newResource);
                await _unitOfWork.SaveChangesAsync();
            }

            var quantity = !notification.IsCreatedCall ? notification.Quantity : 0;

            if (quantity != 0)
            {
                var wareStock = _unitOfWork.WareStockRepository.Get(filter: x => x.WareId.Equals(notification.WareId) && x.ResourceId.Equals(notification.ResourceId)).FirstOrDefault();
                if (wareStock != null)
                {
                    wareStock.Quantity += quantity;
                    _unitOfWork.WareStockRepository.Update(wareStock);
                }
                else
                {
                    wareStock = new WareStock
                    {
                        WareId = notification.WareId,
                        ResourceId = notification.ResourceId,
                        Quantity = quantity
                    };
                    _unitOfWork.WareStockRepository.Insert(wareStock);
                }
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
