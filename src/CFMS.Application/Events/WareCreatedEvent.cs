using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Events
{
    public class WareCreatedEvent : INotification
    {
        public Guid? FarmId { get; set; }

        public WareCreatedEvent(Guid? farmId)
        {
            FarmId = farmId;
        }
    }
}
