using CFMS.Domain.Core.Commands;
using CFMS.Domain.Core.Events;

namespace CFMS.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command)
        where T : ICommand;

        Task RaiseEvent<T>(T @event)
            where T : IEvent;
    }
}
