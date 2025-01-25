using CFMS.Domain.Core.Commands;
using CFMS.Domain.Core.Events;
using MediatR;

namespace CFMS.Domain.Core.Bus
{
    public class Bus : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public Bus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task RaiseEvent<T>(T @event) where T : IEvent
        {
            return _mediator.Publish(@event);
        }

        public Task SendCommand<T>(T command) where T : ICommand
        {
            return _mediator.Send(command);
        }
    }
}
