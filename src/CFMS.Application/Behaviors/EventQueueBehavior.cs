using CFMS.Application.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Behaviors
{
    public class EventQueueBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly EventQueue _eventQueue;

        public EventQueueBehavior(EventQueue eventQueue)
        {
            _eventQueue = eventQueue;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_eventQueue.HasErrors())
            {
                throw new Exception(_eventQueue.PopError());
            }

            return await next();
        }
    }
}
