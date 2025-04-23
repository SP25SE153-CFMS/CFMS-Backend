using Microsoft.AspNetCore.SignalR;

namespace CFMS.Application.Services.SignalR
{
    public class NotiHub : Hub
    {
        private IHubContext<NotiHub> _hubContext;

        public NotiHub(IHubContext<NotiHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendMessage(object data)
        {
            await _hubContext.Clients.All.SendAsync("SendMessage", data);
        }
    }
}
