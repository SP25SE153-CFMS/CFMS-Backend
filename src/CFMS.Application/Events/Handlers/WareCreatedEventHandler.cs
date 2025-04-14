using CFMS.Domain.Entities;
using CFMS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CFMS.Application.Events.Handlers
{
    public class WareCreatedEventHandler : INotificationHandler<WareCreatedEvent>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EventQueue _eventQueue;

        public WareCreatedEventHandler(IUnitOfWork unitOfWork, EventQueue eventQueue)
        {
            _unitOfWork = unitOfWork;
            _eventQueue = eventQueue;
        }


        public async System.Threading.Tasks.Task Handle(WareCreatedEvent notification, CancellationToken cancellationToken)
        {
            var existFarm = _unitOfWork.FarmRepository.Get(filter: x => x.FarmId.Equals(notification.FarmId)).FirstOrDefault();
            if (existFarm == null)
            {
                throw new Exception("Không tìm thấy trang trại");
            }

            foreach (var (code, name, description) in DefaultWares)
            {
                var subCategory = _unitOfWork.SubCategoryRepository
                    .Get(filter: x => x.SubCategoryName.Equals(code))
                    .FirstOrDefault();

                if (subCategory == null)
                {
                    throw new Exception("Không tìm thấy loại kho");
                }

                _unitOfWork.WarehouseRepository.Insert(new Warehouse
                {
                    FarmId = notification.FarmId,
                    ResourceTypeId = subCategory.SubCategoryId,
                    WarehouseName = name,
                    MaxQuantity = 20000,
                    MaxWeight = 50000,
                    CurrentQuantity = 0,
                    CurrentWeight = 0,
                    Description = description,
                    Status = 1
                });

                await _unitOfWork.SaveChangesAsync();
            }
        }

        public static readonly List<(string Code, string Name, string Description)> DefaultWares = new()
        {
            ("food", "Kho thực phẩm", "Chứa thực phẩm cho trang trại"),
            ("medicine", "Kho dược phẩm", "Chứa dược phẩm cho trang trại"),
            ("equipment", "Kho thiết bị", "Chứa thiết bị cho trang trại"),
            ("havest_product", "Kho thu hoạch", "Chứa sản phẩm đã thu hoạch"),
            ("breeding", "Kho con giống", "Chứa con giống")
        };
    }
}
