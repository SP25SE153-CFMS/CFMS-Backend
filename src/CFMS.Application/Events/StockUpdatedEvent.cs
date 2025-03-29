using CFMS.Application.Common;
using CFMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Events
{
    public class StockUpdatedEvent : INotification
    {
        public StockUpdatedEvent(Guid? wareId, Guid? resourceId, int quantity, Guid? unitId, Guid? resourceTypeId, Guid? packageId, decimal? packageSize)
        {
            WareId = wareId;
            ResourceId = resourceId;
            Quantity = quantity;
            UnitId = unitId;
            ResourceTypeId = resourceTypeId;
            PackageId = packageId;
            PackageSize = packageSize;
        }

        public bool IsCreatedCall { get; set; }

        public Guid? WareId { get; set; }

        public Guid? ResourceId { get; set; }

        public Guid? ResourceTypeId { get; set; }

        public int Quantity { get; set; }

        public Guid? UnitId { get; set; }

        public Guid? PackageId { get; set; }

        public decimal? PackageSize { get; set; }
    }
}
