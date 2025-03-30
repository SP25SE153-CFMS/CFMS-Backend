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
        public StockUpdatedEvent(Guid resourceId, int quantity, Guid? unitId, string resourceType, Guid? packageId, decimal? packageSize, Guid wareId, bool isCreatedCall)
        {
            ResourceId = resourceId;
            Quantity = quantity;
            UnitId = unitId;
            ResourceType = resourceType;
            PackageId = packageId;
            PackageSize = packageSize;
            WareId = wareId;
            IsCreatedCall = isCreatedCall;
        }

        public bool IsCreatedCall { get; set; }

        public Guid WareId { get; set; }

        public Guid ResourceId { get; set; }

        public string ResourceType { get; set; }

        public int Quantity { get; set; }

        public Guid? UnitId { get; set; }

        public Guid? PackageId { get; set; }

        public decimal? PackageSize { get; set; }
    }
}
