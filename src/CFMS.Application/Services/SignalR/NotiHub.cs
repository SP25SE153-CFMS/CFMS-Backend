using Microsoft.AspNetCore.SignalR;

namespace CFMS.Application.Services.SignalR
{
    public class NotiHub : Hub
    {
        public async Task SendMessage(object data)
        {
            await Clients.All.SendAsync("ReceiveMessage", data);
        }
    }
}
